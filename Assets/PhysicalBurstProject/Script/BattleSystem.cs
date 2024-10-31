using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Triggers;
using ModestTree;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

[RequireComponent(typeof(PlayerInput))]
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
    private BattleStartUIPrinter battleStartUIPrinter;

    [Inject]
    private LastConfirmSystem lastConfirmSystem;

    [Inject]
    private BGMPlayable bgmPlayer;

    private CancellationToken cts;

    private bool isConfirm;
    private bool isCancel;

    private int cmdIndex;

    private bool isBattleEnd;

    private PlayerInput input;

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

    private async UniTask Battle()
    {
        Debug.Log("BattleStart");
        await BattleStart();

        while (!isBattleEnd)
        {
            await TurnStart();

            foreach (var p in pawns)
            {
                cameraChanger.ChangeToPawnCamera(p.ID);
                p.SelectStart();

                Debug.Log("pawnID: " + p.ID);
                if (p.Type == PawnType.Enemy)
                {
                    IEnemyPawn enemy = strage.GetPawnByID<IEnemyPawn>(p.ID);
                    enemy.EnemySelect();
                    
                    p.SelectEnd();
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
            }

            await TurnEnd();
        }
        bgmPlayer.StopBGM();
        return;
    }

    private async UniTask BattleStart()
    {
        await battleStartUIPrinter.PrintUIAndWait();

        await UniTask.WaitUntil(() => strage.IsSetComplete, PlayerLoopTiming.Update, cts);

        bgmPlayer.PlayBGM();

        pawns = strage.GetPawnList<ActionSelectable>();
        Debug.Log("pawn get:" + pawns.Length);

        isBattleEnd = false;

        await UniTask.Delay(100);

        await UniTask.WaitUntil(() => cameraChanger.IsSetComplete, cancellationToken: cts);
        return;
    }

    private async UniTask TurnStart()
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
            Debug.Log("len:" + pawn.GetActionNames().Length);

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

    private async UniTask TurnEnd()
    {
        foreach (var p in pawns) await p.TurnEnd();
        return;
    }

    private void Awake()
    {
        input = GetComponent<PlayerInput>();
    }


    // Start is called before the first frame update
    void Start()
    {
        cts = this.GetCancellationTokenOnDestroy();
        Battle().Forget();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
