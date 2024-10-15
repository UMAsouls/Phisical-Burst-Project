using Cysharp.Threading.Tasks;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class CmdSelectSystem : ICmdSelectSystem
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

    private int cmdIndex = 0;
    private int cmdLength = 0;

    private bool isConfirm = false;

    private bool isCancel = false;

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

    public async UniTask<bool> CmdSelect(int id)
    {
        CommandActionSettable pawn = strage.GetPawnById<CommandActionSettable>(id);

        IActionCommand[] commands = pawn.GetActionCommands();
        cmdLength = commands.Length;

        string[] names  = new string[commands.Length];
        for (int i = 0; i < commands.Length; i++)  names[i] = commands[i].Name;

        controller = uiPrinter.PrintCmdSelecter(names);

        await UniTask.WaitUntil(() => isConfirm | isCancel);

        if (isCancel) return false;

        pawn.ActionAdd(await MakeAction(commands[cmdIndex]));

        return true;
    }

    private async UniTask<IAction> MakeAction(IActionCommand cmd)
    {
        var behaviour = await behaviourMaker.MakeCommandBehaviour(cmd);
        return actionMaker.MakeCommandAction(behaviour);
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}