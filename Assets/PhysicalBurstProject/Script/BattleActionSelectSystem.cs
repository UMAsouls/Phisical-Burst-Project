using Cysharp.Threading.Tasks;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.InputSystem;
using Zenject;
using UnityEngine.Windows;
using UnityEditor;
using System.Threading;

public class BattleActionSelectSystem : ConfirmCancelCatchAble, IBattleActionSelectSystem
{
    [Inject]
    private ICmdSelectUIPrinter uiPrinter;

    [Inject]
    private SystemSEPlayable sePlayer;

    private string[] defaultActions =
    {
        "移動", "襲撃", "待ち伏せ", "行動"
    };

    private int cmdIndex = 0;
    private int cmdLength = 4;

    private ICmdSelectorController controller;

    private CancellationToken cts;

    protected override InputMode SelfMode => InputMode.FirstSelect;

    public void OnSelectorMove(InputAction.CallbackContext context)
    {
        Vector2 moveInput = context.ReadValue<Vector2>();

        if (!context.performed) return;

        sePlayer.SelectorMoveSE();

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


    }

    public async UniTask<int> BattleActionSelect(int id)
    {
        isCancel = false;
        isConfirm = false;
        cmdIndex = 0;
        cmdLength = 4;

        controller = uiPrinter.PrintCmdSelecter(defaultActions);

        InputModeChangeToSelf();

        await UniTask.WaitUntil(() => (isCancel || isConfirm), PlayerLoopTiming.Update, cts);

        if (isCancel) return -1;

        uiPrinter.DestroyCmdSelector();
        controller = null;

        return cmdIndex;
    }

    // Use this for initialization
    public override void Start()
    {
        cts = this.GetCancellationTokenOnDestroy();
        SetAction("Move", OnSelectorMove);
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {

    }
}