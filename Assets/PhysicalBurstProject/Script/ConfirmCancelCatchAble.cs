using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class ConfirmCancelCatchAble : MonoBehaviour
{

    protected bool isConfirm;

    protected bool isCancel;

    public void OnConfirm(InputAction.CallbackContext context)
    {
        if (context.performed) isConfirm = true;
    }

    public void OnCancel(InputAction.CallbackContext context)
    {
        if (context.performed) isCancel = true;
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