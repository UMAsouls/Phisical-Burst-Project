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

    [SerializeField]
    string FirstScene = "Tutorial";

    private ICmdSelectorController controller;

    private int idx;

    protected override InputMode SelfMode => InputMode.Title;

    public void OnSelectorMove(InputAction.CallbackContext context)
    {
        Vector2 moveInput = context.ReadValue<Vector2>();

        if (!context.started) return;
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

    protected override void SetAllAction()
    {
        SetAction("Move", OnSelectorMove);
        base.SetAllAction();
    }


    // Use this for initialization
    public override void Start()
    {
        idx = 0;
        controller = GetComponentInChildren<ICmdSelectorController>();
        
        isCancel = false;
        isConfirm = false;

        base.Start();
        InputModeChangeToSelf();
    }

    // Update is called once per frame
    void Update()
    {
        if(isConfirm)
        {
            switch(idx)
            {
                case 0:
                    SceneManager.LoadScene(FirstScene);
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