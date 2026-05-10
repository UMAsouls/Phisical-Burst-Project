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

    [SerializeField]
    Vector2 MoveLimit = new Vector2(40,30);

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
        MoveLimit = new Vector2(40, 30);
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

        float min_limit_x = -MoveLimit.x / 2;
        float max_limit_x = MoveLimit.x / 2;
        float min_limit_y = -MoveLimit.y / 2;
        float max_limit_y = MoveLimit.y /2;

        transform.position = new Vector3(
            Mathf.Clamp(x, min_limit_x, max_limit_x),
            Mathf.Clamp(y, min_limit_y, max_limit_y),
            -1);

        transform.position = new Vector3(transform.position.x, transform.position.y, -1);
        cameraController.Position = transform.position;
    }
}
