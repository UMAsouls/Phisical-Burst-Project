using Cysharp.Threading.Tasks;
using System.Collections;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.InputSystem;
using Zenject;
using UnityEngine.Windows;

public class BattleCmdSelectSystem : ConfirmCancelCatchAble, IBattleCmdSelectSystem
{
    [Inject]
    IPawnGettable strage;

    private CancellationToken token;

    [Inject]
    private ICmdSelectUIPrinter selectUIPrinter;

    private ICmdSelectorController selectorController;

    private int cmdIndex = 0;
    private int cmdLength = 0;

    [Inject]
    private IBattleCmdSlotUIPrinter slotUIPrinter;

    private SlotWindowControlable slotController;

    private BattleCmdSelectable pawn;

    [Inject]
    private ICmdInfoUIPrinter cmdInfoPrinter;

    [Inject]
    LastConfirmSystem lastConfirmSystem;


    private int selectCount;

    protected override InputMode SelfMode => InputMode.BattleCmdSelect;

    public void OnSelectorMove(InputAction.CallbackContext context)
    {
        Vector2 moveInput = context.ReadValue<Vector2>();

        if (!context.performed) return;

        systemSEPlayer.SelectorMoveSE();

        //input上ではup > 0 down < 0
        if (moveInput.y > 0)
        {
            selectorController.Move(-1);
            cmdIndex = (int)Mathf.Repeat(cmdIndex - 1, cmdLength);
        }

        if (moveInput.y < 0)
        {
            selectorController.Move(1);
            cmdIndex = (int)Mathf.Repeat(cmdIndex + 1, cmdLength);
        }

        cmdInfoPrinter.PrintUI(pawn.BattleCommands[cmdIndex]);
    }

    private string[] MakeCmdList(ICommand[] cmds)
    {
        string[] ans = new string[cmds.Length];
        for (int i = 0; i < cmds.Length; i++)
        {
            ans[i] = cmds[i].Name;
        }
        return ans;
    }

    private void CancelCmd(IBattleCommand[] ans)
    {
        if(selectCount == 0) return;

        selectCount--;
        pawn.VirtualMana += ans[selectCount].UseMana;
        
        ans[selectCount] = null;
        slotController.ActionSet("", selectCount);
    }

    public async UniTask<IBattleCommand[]> Select(int pawnID)
    {
        inputModeBroker.BroadCast(InputModeTopic.SwitchActionMap, SelfMode);

        pawn = strage.GetPawnByID<BattleCmdSelectable>(pawnID);

        selectorController = selectUIPrinter.PrintCmdSelecter(MakeCmdList(pawn.BattleCommands));
        slotController = slotUIPrinter.PrintUI();

        selectCount = 0;

        cmdIndex = 0;
        cmdLength = pawn.BattleCommands.Length;

        cmdInfoPrinter.PrintUI(pawn.BattleCommands[cmdIndex]);

        IBattleCommand[] ans = new IBattleCommand[3];
        while(selectCount < 3)
        {
            isCancel = false;
            isConfirm = false;

            await UniTask.WaitUntil(() => (isConfirm | isCancel), cancellationToken: token);

            if(isCancel)
            {
                if (selectCount <= 0) { ans = null; break; }
                CancelCmd(ans);
                continue;
            }

            IBattleCommand cmd = pawn.BattleCommands[cmdIndex];
            if(pawn.VirtualMana < cmd.UseMana)
            {
                systemSEPlayer.BlockSE();
                continue;
            }

            pawn.VirtualMana -= cmd.UseMana;
            ans[selectCount] = cmd;
            slotController.ActionSet(cmd.Name, selectCount);
            selectCount++;

            if (selectCount == 3)
            {
                if (! await lastConfirmSystem.ConfirmWait())
                {
                    CancelCmd(ans);
                    inputModeBroker.BroadCast(InputModeTopic.SwitchActionMap, SelfMode);
                } 
            }
 
        }

        selectUIPrinter.DestroyCmdSelector();
        slotUIPrinter.DestroyUI();
        cmdInfoPrinter.DestroyUI();

        return ans;
    }

    protected override void SetAllAction()
    {
        SetAction("Move", OnSelectorMove);
        base.SetAllAction();
    }

    // Use this for initialization
    public override void Start()
    {
        token = this.GetCancellationTokenOnDestroy();

        base.Start();
    }

    // Update is called once per frame
    void Update()
    {

    }
}