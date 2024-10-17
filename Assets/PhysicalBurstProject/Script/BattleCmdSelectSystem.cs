using Cysharp.Threading.Tasks;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.InputSystem;
using Zenject;
using UnityEngine.Windows;
using UnityEditor;

public class BattleCmdSelectSystem : MonoBehaviour, IBattleCmdSelectSystem
{
    [Inject]
    private ICmdSelectUIPrinter uiPrinter;

    private string[] defaultActions =
    {
        "移動", "襲撃", "待ち伏せ", "行動"
    };

    private int cmdIndex = 0;
    private int cmdLength = 4;

    private bool isConfirm = false;
    private bool isCancel = false;

    private ICmdSelectorController controller;

    private PlayerInput input;

    public void OnSelectorMove(InputAction.CallbackContext context)
    {
        Vector2 moveInput = context.ReadValue<Vector2>();

        if (!context.performed) return;

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

        Debug.Log($"{cmdIndex}");


    }

    public void OnConfirm(InputAction.CallbackContext context)
    {
        if (context.performed) isConfirm = true;
    }

    public void OnCancel(InputAction.CallbackContext context)
    {
        if (context.performed) isCancel = true;
    }

    public async UniTask<int> BattleCmdSelect(int id)
    {
        isCancel = false;
        isConfirm = false;
        cmdIndex = 0;
        cmdLength = 4;

        controller = uiPrinter.PrintCmdSelecter(defaultActions);

        input.SwitchCurrentActionMap("FirstSelect");

        await UniTask.WaitUntil(() => isConfirm | isCancel);

        if (isCancel) return -1;

        uiPrinter.DestroyCmdSelector();
        controller = null;
        return cmdIndex;
    }

    private void Awake()
    {
        input = gameObject.GetComponent<PlayerInput>();
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