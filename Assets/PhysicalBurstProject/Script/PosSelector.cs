using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class PosSelector : MonoBehaviour, PosSelectorRangeSetter
{
    [Inject]
    private PosConfirmAble posConfirmAble;

    private Vector2 movedir;

    [SerializeField]
    float moveSpeed = 1;

    private float range;

    private Vector2 firstPos;

    public float Range { set => range = value; }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

       movedir = context.ReadValue<Vector2>();
    }

    public void OnConfirm(InputAction.CallbackContext context)
    {
        if (context.performed) posConfirmAble.PosConfirm(transform.position);
    }

    public void OnCancel(InputAction.CallbackContext context)
    {
        if(context.performed) posConfirmAble.Cancel();
    }

    // Start is called before the first frame update
    void Start()
    {
        firstPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += (Vector3)movedir;
        if(Vector2.Distance(transform.position, firstPos) > range)
        {
            transform.position -= (Vector3)movedir;
        }
    }
}
