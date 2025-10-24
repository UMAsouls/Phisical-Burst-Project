using Cysharp.Threading.Tasks;
using System.Collections;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class BattleSystem : MonoBehaviour
{
    private string[] defaultActions =
    {
        "à⁄ìÆ", "èPåÇ", "ë“ÇøïöÇπ", "çsìÆ"
    };

    private ActionSelectable[] pawns;

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

    protected CancellationToken cts;

    [SerializeField]
    string nextScene;

    private bool isConfirm;
    private bool isCancel;

    private int cmdIndex;

    private bool isBattleEnd;

    private bool EnemyWin = false;
    private bool PlayerWin = false;

    protected int turn = 0;

    /// <summary>
    /// SpeedGettable[]ÇÃCoparer
    /// ç~èáÇ…É\Å[Ég
    /// </summary>
    class SpeedComparer : IComparer
    {
        public int Compare(object x, object y)
        {
            ActionSelectable sx = (ActionSelectable)x;
            ActionSelectable sy = (ActionSelectable)y;

            return (new CaseInsensitiveComparer()).Compare(sy.speed, sx.speed);
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

    protected async UniTask Battle()
    {
        Debug.Log("BattleStart");
        await BattleStart();

        while (!isBattleEnd)
        {
            turn++;
            await TurnStart();

            foreach (var p in pawns)
            {
                if(p == null || p.Death) continue;

                cameraChanger.ChangeToPawnCamera(p.ID);
                if (p.IsStun) continue;

                p.SelectStart();

                Debug.Log("pawnID: " + p.ID);
                if (p.Type == PawnType.Enemy)
                {
                    IEnemyPawn enemy = strage.GetPawnByID<IEnemyPawn>(p.ID);
                    enemy.EnemySelect();
                    
                    p.SelectEnd();
                    Debug.Log("selectEnd");
                    await p.DoAction();
                    continue;
                }

                do
                {
                    while (p.ActPoint > 0)
                    {
                        Debug.Log("Select phaze");
                        do
                        {
                            isCancel = false;
                            await Select(p);
                        } while (isCancel);
                    }
                    slotPrint(p.GetActionNames());
                    if (! await lastConfirmSystem.ConfirmWait())
                    {
                        isCancel=true;
                        p.CancelSelect();
                    }
                    slotPrinter.DestroyActionSlot();

                } while(isCancel);

                p.SelectEnd();
                await p.DoAction();

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

    protected virtual async UniTask BattleStart()
    {
        cameraChanger.ChangeToCenterCamera();

        await UniTask.Delay(300, cancellationToken: destroyCancellationToken);
        await battleStartUIPrinter.PrintStartUIAndWait();

        await UniTask.WaitUntil(() => strage.IsSetComplete, PlayerLoopTiming.Update, cts);

        bgmPlayer.PlayBGM();

        pawns = strage.GetPawnList<ActionSelectable>();
        Debug.Log("pawn get:" + pawns.Length);

        isBattleEnd = false;

        await UniTask.Delay(100, cancellationToken: destroyCancellationToken);

        await UniTask.WaitUntil(() => cameraChanger.IsSetComplete, cancellationToken: cts);
        return;
    }

    protected virtual async UniTask TurnStart()
    {
        System.Array.Sort(pawns, new SpeedComparer());
        foreach (var p in pawns)
        {
            await p.TurnStart();
        }
        Debug.Log("Turn Start");
        return;
    }

    private async UniTask Select(ActionSelectable pawn)
    {
        do
        {
            uiPrinter.PrintPlayerInformation(pawn.ID);
            slotPrint(pawn.GetActionNames());

            isConfirm = false;
            isCancel = false;
            bool actCancel = false;
            cmdIndex = await battleCmdSelectSystem.BattleActionSelect(pawn.ID);

            slotPrinter.DestroyActionSlot();
            uiPrinter.DestroyPlayerInformation();
            switch (cmdIndex)
            {
                case -1:
                    actCancel= true;
                    break;
                case 0:
                    isConfirm = await MovePosSelectable.MovePosSelect(pawn.ID);
                    break;
                case 1:
                    isConfirm = await battleCmdActionSelectSystem.Select(pawn.ID);
                    break;
                case 2:
                    isConfirm = await ambushSelectSystem.AmbushSelect(pawn.ID);
                    break;
                case 3:
                    isConfirm = await cmdSelectSystem.CmdSelect(pawn.ID);
                    break;
            }

            if(actCancel)
            {
                pawn.CancelSelect();
                isCancel = true;
                break;
            }

            if (!isConfirm) isCancel = true;

        } while(isCancel);
        return;
    }

    protected virtual async UniTask TurnEnd()
    {
        int count = 0;
        foreach (var p in pawns) if(!p.Death) count++;

        ActionSelectable[] newList = new ActionSelectable[count];
        int idx = 0;
        for(int i = 0; i < pawns.Length; i++)
        {
            if (!pawns[i].Death) newList[idx++] = pawns[i];
        }
        pawns = newList;

        foreach (var p in pawns)
        {     
            await p.TurnEnd();
        }

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
}
