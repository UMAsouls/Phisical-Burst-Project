using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public abstract class CommandMakerBase<T> : ConfirmCancelCatchAble
{
    [Inject]
    protected IPawnGettable strage;

    [Inject]
    protected CameraChangeAble cameraChanger;

    [Inject]
    protected SelectPhazeCameraControllable cameraController;

    protected bool isBurst = false;

    protected PlayerInput input;

    protected string actionMap;

    protected OrthoCameraZoomAble cameraZoomController;

    public void OnBurst(InputAction.CallbackContext context)
    {
        if(context.performed) isBurst = !isBurst;
    }

    public virtual void Init(int id)
    {
        var p = strage.GetPawnById<IVirtualPawn>(id);
        isConfirm = false;
        isCancel = false;
        isBurst = false;

        cameraChanger.ChangeToSelectPhazeCamera();
        cameraController.Position = p.VirtualPos;

        cameraZoomController = cameraChanger.GetZoomController();

        input.SwitchCurrentActionMap(actionMap);
    }

    public void End(int id)
    {
        cameraChanger.ChangeToPawnCamera(id);
        input.SwitchCurrentActionMap("None");
    }

    public async UniTask<IActionCommandBehaviour> GetBehaviour(T cmd, int pawnID)
    {
        Init(pawnID);
        var b = await MakeBehaviour(cmd, pawnID);
        End(pawnID);
        return b;
    }

    public abstract UniTask<IActionCommandBehaviour> MakeBehaviour(T cmd, int pawnID);

    protected virtual void Awake()
    {
        input = GetComponent<PlayerInput>();
    }
}