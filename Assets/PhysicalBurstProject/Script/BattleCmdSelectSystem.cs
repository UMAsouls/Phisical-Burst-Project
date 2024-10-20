using Cysharp.Threading.Tasks;
using System.Collections;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.InputSystem;
using Zenject;

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

    private PlayerInput input;


    private int selectCount;

    public void OnSelectorMove(InputAction.CallbackContext context)
    {
        Vector2 moveInput = context.ReadValue<Vector2>();

        if (!context.performed) return;

        //input上ではup > 0 down < 0
        if (moveInput.y > 0)
        {
            selectorController.Move(-1);
            cmdIndex = (int)Mathf.Repeat(cmdIndex - 1, cmdLength - 1);
        }

        if (moveInput.y < 0)
        {
            selectorController.Move(1);
            cmdIndex = (int)Mathf.Repeat(cmdIndex + 1, cmdLength - 1);
        }
    }

    private string[] MakeCmdList(ICommand[]cmds)
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
        input.SwitchCurrentActionMap("BattleCmdSelect");

        pawn = strage.GetPawnByID<BattleCmdSelectable>(pawnID);

        selectorController = selectUIPrinter.PrintCmdSelecter(MakeCmdList(pawn.BattleCommands));
        slotController = slotUIPrinter.PrintUI();

        selectCount = 0;

        cmdIndex = 0;
        cmdLength = pawn.BattleCommands.Length;

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
                continue;
            }

            pawn.VirtualMana -= cmd.UseMana;
            ans[selectCount] = cmd;
            slotController.ActionSet(cmd.Name, selectCount);
            selectCount++;
        }

        selectUIPrinter.DestroyCmdSelector();
        slotUIPrinter.DestroyUI();
        input.SwitchCurrentActionMap("BattleCmdSelect");

        return ans;
    }

    // Use this for initialization
    void Start()
    {
        token = this.GetCancellationTokenOnDestroy();
        input = GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}