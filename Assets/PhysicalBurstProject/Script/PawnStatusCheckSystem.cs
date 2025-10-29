using Cysharp.Threading.Tasks;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class PawnStatusCheckSystem : ConfirmCancelCatchAble, 
    IPawnStatusCheckSystem
{
    [Inject]
    private IPawnGettable strage;

    [Inject]
    private IBroker<UIControlTopic, StatusUIMessage> broker;

    [Inject]
    private IPosSelectorUIPrinter posSelectorUIPrinter;

    [Inject]
    private CameraChangeAble cameraChanger;

    [Inject]
    private CameraControllable cameraController;

    private OrthoCameraZoomAble zoomController;

    [SerializeField]
    private float zoom;

    [SerializeField]
    private float checkRange;

    private bool onSelect;

    private SelectedPawn pawn;

    protected override InputMode SelfMode => InputMode.PawnSelect;

    public void OnMove(InputAction.CallbackContext context)
    {
        if (!onSelect) return;
        Vector2 movedir = context.ReadValue<Vector2>();
        cameraController.SetMoveDir(movedir);
    }

    public void End(int pawnID)
    {
        onSelect = false;
        cameraChanger.ChangeToPawnCamera(pawnID);
        posSelectorUIPrinter.DestroyPosSelectorUI();
        cameraController.RangeMode = false;
        if (pawn != null) { UnSelect(pawn); pawn = null; }
    }

    protected override void SetAllAction()
    {
        SetAction("Move", OnMove);
        base.SetAllAction();
    }

    protected void Init(Vector2 startPos)
    {
        isConfirm = false;
        isCancel = false;

        posSelectorUIPrinter.PrintPosSelectorUI();

        cameraChanger.ChangeToMovableCamera();
        zoomController = cameraChanger.GetZoomController();

        zoomController.OrthoSize = zoom;

        cameraController.SetFirstPos(startPos);
        cameraController.Range = checkRange;
        cameraController.RangeMode = true;

        onSelect = true;

        InputModeChangeToSelf();
    }

    public async UniTask PawnStatusCheck(int id)
    {
        IPawnInfo p = strage.GetPawnByID<IPawnInfo>(id);
        var startPos = p.Position;

        Init(startPos);

        await UniTask.WaitUntil(
            () => isCancel, 
            cancellationToken: destroyCancellationToken
        );

        End(id);
    }

    private void Select(SelectedPawn p)
    {
        p.SelectedFocus();

        IStatus s = strage.GetPawnByID<IStatusGettable>(p.ID).Status;
        StatusUIMessage message = new StatusUIMessage(false, s);
        
        broker.BroadCast(UIControlTopic.PawnStatus, message);
    }

    private void UnSelect(SelectedPawn p)
    {
        p.SelectedUnFocus();

        IStatus s = strage.GetPawnByID<IStatusGettable>(p.ID).Status;
        StatusUIMessage message = new StatusUIMessage(true, s);

        broker.BroadCast(UIControlTopic.PawnStatus, message);
    }

    private void RayHit(Collider2D collider)
    {
        var obj = collider.gameObject.transform.root;
        var p2 = obj.GetComponent<SelectedPawn>();
        if (p2 == null) return;

        if (pawn != p2)
        {
            if (pawn != null) UnSelect(pawn);
            Select(p2);
        }
        pawn = p2;
    }

    // Update is called once per frame
    void Update()
    {
        if (!onSelect) return;

        Vector3 screenCenter = new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0.0f);
        Vector3 center = Camera.main.ScreenToWorldPoint(screenCenter);
        //center.y += 0.5f;

        RaycastHit2D hit = Physics2D.Raycast(center, Vector2.down, 0f);

        if (hit.collider != null) RayHit(hit.collider);
        else if (pawn != null)
        {
            UnSelect(pawn);
            pawn = null;
        }
    }
}