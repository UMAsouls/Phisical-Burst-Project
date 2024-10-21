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
    private IPawnGettable pawnGettable;

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

                await UniTask.Delay(100);
                Debug.Log($"Turn {p.ID}");

                if (p.Type == PawnType.Enemy)
                {
                    continue;
                }

                while (p.ActPoint > 0)
                {
                    
                    Debug.Log("Select phaze");
                    do
                    {

                        isCancel = false;
                        await Select(p);
                    } while (isCancel);
                }  
            }
            await TurnEnd();
        }
        return;
    }

    private async UniTask BattleStart()
    {
        await UniTask.WaitUntil(() => pawnGettable.IsSetComplete, PlayerLoopTiming.Update, cts);

        pawns = pawnGettable.GetPawnList<ActionSelectable>();
        Debug.Log("pawn get:" + pawns.Length);

        isBattleEnd = false;

        await UniTask.Delay(100);

        await UniTask.WaitUntil(() => cameraChanger.IsSetComplete, cancellationToken: cts);
        return;
    }

    private async UniTask TurnStart()
    {
        System.Array.Sort(pawns, new SpeedComparer());
        int i = 0;
        foreach (var p in pawns)
        {
            await p.TurnStart();
        }
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
