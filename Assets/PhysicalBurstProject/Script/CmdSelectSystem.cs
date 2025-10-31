using Cysharp.Threading.Tasks;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class CmdSelectSystem : ConfirmCancelCatchAble,ICmdSelectSystem
{
    [Inject]
    IPawnGettable strage;

    [Inject]
    ICmdSelectUIPrinter uiPrinter;

    [Inject]
    CommandActionMakeable actionMaker;

    [Inject]
    CommandBehaviourMakeable behaviourMaker;

    ICmdSelectorController controller;

    [Inject]
    private ICmdInfoUIPrinter cmdInfoPrinter;

    CommandActionSettable pawn;

    private int cmdIndex = 0;
    private int cmdLength = 0;

    private CancellationToken cts;

    protected override InputMode SelfMode => InputMode.ActionCmdSelect;

    public void OnSelectorMove(InputAction.CallbackContext context)
    {
        Vector2 moveInput = context.ReadValue<Vector2>();

        if (!context.started) return;

        systemSEPlayer.SelectorMoveSE();

        //input上ではup > 0 down < 0
        if (moveInput.y > 0)
        {
            controller.Move(-1);
            cmdIndex = (int)Mathf.Repeat(cmdIndex - 1, cmdLength);
        }

        if (moveInput.y < 0)
        {
            controller.Move(1);
            cmdIndex = (int)Mathf.Repeat(cmdIndex + 1, cmdLength);
        }

        cmdInfoPrinter.PrintUI(pawn.GetActionCommands()[cmdIndex]);

    }

    private void Init()
    {
        isCancel = false;
        isConfirm = false;
        cmdIndex = 0;

        InputModeChangeToSelf();
    }

    public virtual async UniTask<bool> CmdSelect(int id)
    {
        Init();

        pawn = strage.GetPawnByID<CommandActionSettable>(id);

        IActionCommand[] commands = pawn.GetActionCommands();
        cmdLength = commands.Length;

        string[] names  = new string[commands.Length];
        for (int i = 0; i < commands.Length; i++)  names[i] = commands[i].Name;

        controller = uiPrinter.PrintCmdSelecter(names);
        Debug.Log($"cmdidx {cmdIndex}, cmdLength: {cmdLength}");
        cmdInfoPrinter.PrintUI(pawn.GetActionCommands()[cmdIndex]);

        do
        {
            isConfirm = false;
            isCancel = false;
            await UniTask.WaitUntil(() => isConfirm | isCancel, PlayerLoopTiming.Update, cts);
            if (isCancel)
            {
                cmdInfoPrinter.DestroyUI();
                uiPrinter.DestroyCmdSelector();
                return false;
            }
            if(pawn.VirtualMana >= commands[cmdIndex].UseMana) break;
            systemSEPlayer.BlockSE();
        } while (true);

        cmdInfoPrinter.DestroyUI();
        uiPrinter.DestroyCmdSelector();

        var action = await MakeAction(commands[cmdIndex], id);
        if(action == null) return false;

        Debug.Log("setted comannd");
        action.setAct(pawn);
        return true;
    }

    private async UniTask<IAction> MakeAction(IActionCommand cmd, int pawnID)
    {
        var behaviour = await behaviourMaker.MakeCommandBehaviour(cmd, pawnID);
        if(behaviour == null) return null;
        return actionMaker.MakeCommandAction(behaviour);
    }

    protected override void SetAllAction()
    {
        SetAction("Move", OnSelectorMove);
        base.SetAllAction();
    }

    // Use this for initialization
    public override void Start()
    {
        cts = this.GetCancellationTokenOnDestroy();
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {

    }
}