using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.InputSystem;
using Zenject.SpaceFighter;
using Zenject;
using UnityEngine.SceneManagement;

public class TitleController : ConfirmCancelCatchAble
{
    [Inject]
    SystemSEPlayable sePlayer;

    private ICmdSelectorController controller;
    private ICmdUI setter;

    private int idx;

    private PlayerInput input;


    public void OnSelectorMove(InputAction.CallbackContext context)
    {
        Vector2 moveInput = context.ReadValue<Vector2>();

        if (!context.performed) return;

        sePlayer.SelectorMoveSE();

        //input上ではup > 0 down < 0
        if (moveInput.y > 0)
        {
            idx = (int)Mathf.Repeat(idx - 1, 2);
            controller.Set(idx);
        }

        if (moveInput.y < 0)
        {
            idx = (int)Mathf.Repeat(idx + 1, 2);
            controller.Set(idx);
        }
    }


    // Use this for initialization
    void Start()
    {
        idx = 0;
        controller = GetComponentInChildren<ICmdSelectorController>();
        setter = GetComponentInChildren<ICmdUI>();

        setter.CmdAdd("プレイ");
        setter.CmdAdd("終了");
        isCancel = false;
        isConfirm = false;

        input = GetComponent<PlayerInput>();
        input.SwitchCurrentActionMap("main");

        controller.Set(0);
    }

    // Update is called once per frame
    void Update()
    {
        if(isConfirm)
        {
            switch(idx)
            {
                case 0:
                    SceneManager.LoadScene("Battle");
                    break;
                case 1:
                    #if UNITY_EDITOR
                        UnityEditor.EditorApplication.isPlaying = false;//ゲームプレイ終了
                    #else
                        Application.Quit();//ゲームプレイ終了
                    #endif
                    break;
            }
        }
    }
}