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
    protected CameraControllable cameraController;

    [Inject]
    protected DiContainer container;

    protected bool isBurst = false;

    protected string actionMap;

    protected OrthoCameraZoomAble cameraZoomController;

    public void OnBurst(InputAction.CallbackContext context)
    {
        if(context.performed) isBurst = !isBurst;
    }

    public virtual void Init(int id)
    {
        var p = strage.GetPawnByID<IVirtualPawn>(id);
        isConfirm = false;
        isCancel = false;
        isBurst = false;

        cameraChanger.ChangeToMovableCamera();
        cameraController.Position = p.VirtualPos;

        cameraZoomController = cameraChanger.GetZoomController();

        InputModeChangeToSelf();
    }

    public void End(int id)
    {
        cameraChanger.ChangeToPawnCamera(id);
    }

    public async UniTask<IActionCommandBehaviour> GetBehaviour(T cmd, int pawnID)
    {
        Init(pawnID);
        var b = await MakeBehaviour(cmd, pawnID);
        End(pawnID);
        return b;
    }

    public abstract UniTask<IActionCommandBehaviour> MakeBehaviour(T cmd, int pawnID);

    public override void Start()
    {
        var burstAction = new ActionSetMessage(SelfMode, "Burst", OnBurst);
        actionSetBroker.BroadCast(ActionSetTopic.SetAction, burstAction);
        base.Start();
    }
}