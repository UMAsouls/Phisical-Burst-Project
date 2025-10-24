using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class PosSelector : ConfirmCancelCatchAble, PosSelectorRangeSetter
{

    [Inject]
    CameraControllable cameraController;

    [Inject]
    private PosConfirmAble posConfirmAble;

    private Vector2 movedir;

    [SerializeField]
    float moveSpeed = 0.05f;

    private float range;

    private Vector2 firstPos;

    public float Range { set => range = value; }

    protected override InputMode SelfMode => InputMode.PosSelect;

    public void OnMove(InputAction.CallbackContext context)
    {
       movedir = context.ReadValue<Vector2>();
    }

    public override void OnConfirm(InputAction.CallbackContext context)
    {
        posConfirmAble.PosConfirm(transform.position);
        base.OnConfirm(context);
    }

    public override void OnCancel(InputAction.CallbackContext context)
    {
        posConfirmAble.Cancel();
        base.OnCancel(context);
    }

    protected override void SetAllAction()
    {
        SetAction("Move", OnMove);
        base.SetAllAction();
    }

    // Start is called before the first frame update
    public override void Start()
    {
        firstPos = transform.position;
        base.Start();

        InputModeChangeToSelf();
    }

    // Update is called once per frame
    void Update()
    {
        float dt = Time.deltaTime;
        transform.position += (Vector3)movedir*moveSpeed*dt;
        if(Vector2.Distance(transform.position, firstPos) > range)
        {
            transform.position = firstPos + ((Vector2)transform.position - firstPos).normalized*range;
        }
        float x = transform.position.x;
        float y = transform.position.y;

        transform.position = new Vector3(Mathf.Clamp(x, -20f, 20f), Mathf.Clamp(y, -15f, 15f), -1);

        transform.position = new Vector3(transform.position.x, transform.position.y, -1);
        cameraController.Position = transform.position;
    }
}
