using Cysharp.Threading.Tasks;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class CmdSelectSystem : MonoBehaviour,ICmdSelectSystem
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

    private PlayerInput input;

    private int cmdIndex = 0;
    private int cmdLength = 0;

    private bool isConfirm = false;

    private bool isCancel = false;

    private CancellationToken cts;

    public void OnSelectorMove(InputAction.CallbackContext context)
    {
        Vector2 moveInput = context.ReadValue<Vector2>();

        if (!context.performed) return;

        //input上ではup > 0 down < 0
        if (moveInput.y > 0)
        {
            controller.Move(-1);
            cmdIndex = (int)Mathf.Repeat(cmdIndex - 1, cmdLength - 1);
        }

        if (moveInput.y < 0)
        {
            controller.Move(1);
            cmdIndex = (int)Mathf.Repeat(cmdIndex + 1, cmdLength - 1);
        }

    }

    public void OnConfirm(InputAction.CallbackContext context)
    {
        if (context.performed) isConfirm = true;
    }

    public void OnCancel(InputAction.CallbackContext context)
    {
        if (context.performed) isCancel = true;
    }

    private void Init()
    {
        isCancel = false;
        isConfirm = false;
        cmdIndex = 0;

        input.SwitchCurrentActionMap("CmdSelect");
    }

    public async UniTask<bool> CmdSelect(int id)
    {
        Init();

        CommandActionSettable pawn = strage.GetPawnByID<CommandActionSettable>(id);

        IActionCommand[] commands = pawn.GetActionCommands();
        cmdLength = commands.Length;

        string[] names  = new string[commands.Length];
        for (int i = 0; i < commands.Length; i++)  names[i] = commands[i].Name;

        controller = uiPrinter.PrintCmdSelecter(names);

        await UniTask.WaitUntil(() => isConfirm | isCancel, PlayerLoopTiming.Update, cts);

        if (isCancel) return false;

        uiPrinter.DestroyCmdSelector();
        input.SwitchCurrentActionMap("None");

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

    // Use this for initialization
    void Start()
    {
        cts = this.GetCancellationTokenOnDestroy();
        input = GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}