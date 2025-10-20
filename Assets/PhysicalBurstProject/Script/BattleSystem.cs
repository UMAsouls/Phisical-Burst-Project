using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

[RequireComponent(typeof(PlayerInput))]
public class BattleSystem : MonoBehaviour, IObservable<TurnPhaseFrag>
{
    private string[] defaultActions =
    {
        "à⁄ìÆ", "èPåÇ", "ë“ÇøïöÇπ", "çsìÆ"
    };

    private GameObject[] pawns;

    [Inject]
    private IPlayerInformationUIPrinter uiPrinter;

    [Inject]
    private IActionSlotPrinter slotPrinter;
    private SlotWindowControlable slotController;

    [Inject]
    private IPawnGettable strage;

    [Inject]
    private MovePosSelectable MovePosSelectable;

    [Inject]
    private IBattleActionSelectSystem battleCmdSelectSystem;

    [Inject]
    private ICmdSelectSystem cmdSelectSystem;

    [Inject]
    private CameraChangeAble cameraChanger;

    [Inject]
    private IBattleCmdActionSelectSystem battleCmdActionSelectSystem;

    [Inject]
    private AmbushSelectSystem ambushSelectSystem;

    [Inject]
    private IStandardUIPritner standardUIPritner;

    [Inject]
    private BattleStartEndUIPrinter battleStartUIPrinter;

    [Inject]
    private ResultUIPrinter resultUIPrinter;

    [Inject]
    private LastConfirmSystem lastConfirmSystem;

    [Inject]
    private BGMPlayable bgmPlayer;

    private CancellationToken cts;

    [SerializeField]
    string nextScene;

    private bool isConfirm;
    private bool isCancel;

    private int cmdIndex;

    private bool isBattleEnd;

    private PlayerInput input;

    private bool EnemyWin = false;
    private bool PlayerWin = false;

    private int turn = 0;

    private List<IObserver<TurnPhaseFrag>> turnPhazeObserver;

    /// <summary>
    /// SpeedGettable[]ÇÃCoparer
    /// ç~èáÇ…É\Å[Ég
    /// </summary>
    class SpeedComparer : IComparer
    {
        public int Compare(object x, object y)
        {
            IBattlePawn bx = (IBattlePawn)x;
            IBattlePawn by = (IBattlePawn)y;

            return (new CaseInsensitiveComparer()).Compare(by.Status.Speed, bx.Status.Speed);
        }
    }


    public void CommandConfirm(int index)
    {
        isConfirm = true;
        cmdIndex = index;
    }

    public void CmdCancel()
    {
        isCancel = true;
    }

    public void slotPrint(string[] actNames)
    {
        slotController = slotPrinter.PrintActionSlot();
        for (int i = 0; i < actNames.Length; i++) slotController.ActionSet(actNames[i], i);
    }

    private async UniTask Battle()
    {
        Debug.Log("BattleStart");
        await BattleStart();

        while (!isBattleEnd)
        {
            turn++;
            await TurnStart();

            foreach (var p in pawns)
            {
                var bpawn = p.GetComponent<IBattlePawn>();
                var status = bpawn.Status;
                var actManager = bpawn.ActionManager;
                var info = p.GetComponent<IPawnBattleInfo>();
                var selectUnit = p.GetComponent<SelectUnit>();

                if(p == null || status.IsDeath) continue;

                cameraChanger.ChangeToPawnCamera(info.ID);
                if (status.IsStun) continue;

                selectUnit.SelectStart();

                Debug.Log("pawnID: " + info.ID);
                if (info.Type == PawnType.Enemy)
                {
                    IEnemyPawn enemy = strage.GetPawnComponentByID<IEnemyPawn>(info.ID);
                    enemy.EnemySelect();
                    
                    selectUnit.SelectEnd();
                    Debug.Log("selectEnd");
                    await actManager.DoAction();
                    continue;
                }

                do
                {
                    while (actManager.ActPoint > 0)
                    {
                        Debug.Log("Select phaze");
                        do
                        {
                            isCancel = false;
                            await Select(info.ID, actManager, selectUnit);
                        } while (isCancel);
                    }
                    slotPrint(actManager.GetActionNames());
                    if (! await lastConfirmSystem.ConfirmWait())
                    {
                        isCancel=true;
                        selectUnit.CancelSelect();
                    }
                    slotPrinter.DestroyActionSlot();

                } while(isCancel);

                selectUnit.SelectEnd();
                await actManager.DoAction();

                isBattleEnd = IsBattleEnd();
                if(isBattleEnd) break;
            }

            await TurnEnd();
        }
        bgmPlayer.StopBGM();
        cameraChanger.ChangeToCenterCamera();
        await UniTask.Delay(300, cancellationToken:destroyCancellationToken);
        if(PlayerWin)
        {
            await battleStartUIPrinter.PrintWinUIAndWait();
            resultUIPrinter.PrintWinResult(turn, nextScene);
        }else
        {
            await battleStartUIPrinter.PrintLoseUIAndWait();
            resultUIPrinter.PrintLoseResult(pawns.Length, nextScene);
        }
        return;
    }

    private async UniTask BattleStart()
    {
        cameraChanger.ChangeToCenterCamera();

        await UniTask.Delay(300);
        await battleStartUIPrinter.PrintStartUIAndWait();

        await UniTask.WaitUntil(() => strage.IsSetComplete, PlayerLoopTiming.Update, cts);

        bgmPlayer.PlayBGM();

        pawns = strage.GetPawnObjects();
        Debug.Log("pawn get:" + pawns.Length);

        isBattleEnd = false;

        await UniTask.Delay(100);

        await UniTask.WaitUntil(() => cameraChanger.IsSetComplete, cancellationToken: cts);
        return;
    }

    private async UniTask TurnStart()
    {
        System.Array.Sort(pawns, new SpeedComparer());
        ObserverSupport.BroadCastMessage(
            turnPhazeObserver, TurnPhaseFrag.TurnStart
         );
        Debug.Log("Turn Start");
        return;
    }

    private async UniTask Select(int id, IPawnActionManager actManager, SelectUnit selectUnit)
    {
        do
        {
            uiPrinter.PrintPlayerInformation(id);
            slotPrint(actManager.GetActionNames());
            Debug.Log("len:" + actManager.GetActionNames().Length);

            isConfirm = false;
            isCancel = false;
            bool actCancel = false;
            cmdIndex = await battleCmdSelectSystem.BattleActionSelect(id);

            slotPrinter.DestroyActionSlot();
            uiPrinter.DestroyPlayerInformation();
            switch (cmdIndex)
            {
                case -1:
                    actCancel= true;
                    break;
                case 0:
                    isConfirm = await MovePosSelectable.MovePosSelect(id);
                    break;
                case 1:
                    isConfirm = await battleCmdActionSelectSystem.Select(id);
                    break;
                case 2:
                    isConfirm = await ambushSelectSystem.AmbushSelect(id);
                    break;
                case 3:
                    isConfirm = await cmdSelectSystem.CmdSelect(id);
                    break;
            }

            if(actCancel)
            {
                selectUnit.CancelSelect();
                isCancel = true;
                break;
            }

            if (!isConfirm) isCancel = true;

        } while(isCancel);
        return;
    }

    private async UniTask TurnEnd()
    {

        ObserverSupport.BroadCastMessage(
            turnPhazeObserver, TurnPhaseFrag.TurnEnd
         );

        isBattleEnd = IsBattleEnd();
        return;
    }

    public bool IsBattleEnd()
    {
        EnemyWin = true;
        PlayerWin = true;

        foreach (var p in pawns)
        {
            if(p.Death) continue;

            if(p.Type == PawnType.Member) EnemyWin = false;
            else if(p.Type == PawnType.Enemy) PlayerWin = false;

            Debug.Log(p.name);
        }

        return PlayerWin || EnemyWin;
    }

    private void Awake()
    {
        input = GetComponent<PlayerInput>();
        turnPhazeObserver = new List<IObserver<TurnPhaseFrag>>();
    }


    // Start is called before the first frame update
    void Start()
    {
        turn = 0;
        cts = this.GetCancellationTokenOnDestroy();
        Battle().Forget();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Subscribe(IObserver<TurnPhaseFrag> observer)
    {
        
    }
}
