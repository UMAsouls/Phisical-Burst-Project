using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class ConfirmCancelCatchAble : MonoBehaviour
{

    protected bool isConfirm;

    protected bool isCancel;

    [Inject]
    protected SystemSEPlayable systemSEPlayer;

    public void OnConfirm(InputAction.CallbackContext context)
    {
        if (context.performed) { isConfirm = true; systemSEPlayer.ConfirmSE(); };
    }

    public void OnCancel(InputAction.CallbackContext context)
    {
        if (context.performed) { isCancel = true; systemSEPlayer.CancelSE(); }
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