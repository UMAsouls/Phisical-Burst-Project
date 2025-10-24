using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public abstract class ConfirmCancelCatchAble : InputActionSetter
{

    protected bool isConfirm;

    protected bool isCancel;

    [Inject]
    protected SystemSEPlayable systemSEPlayer;

    public virtual void OnConfirm(InputAction.CallbackContext context)
    {
        if (context.performed) { isConfirm = true; systemSEPlayer.ConfirmSE(); };
    }

    public virtual void OnCancel(InputAction.CallbackContext context)
    {
        if (context.performed) { isCancel = true; systemSEPlayer.CancelSE(); }
    }

    protected override void SetAllAction()
    {
        SetAction("Confirm", OnConfirm);
        SetAction("Cancel", OnCancel);
    }

    // Use this for initialization
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {

    }
}