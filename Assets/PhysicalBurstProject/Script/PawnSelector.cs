using Cysharp.Threading.Tasks;
using System.Collections;
using UnityEngine;
using Zenject;
using System.Threading;
using UnityEngine.InputSystem;

public class PawnSelector : ConfirmCancelCatchAble, IPawnSelector
{
    [Inject]
    private IPawnGettable strage;

    [Inject]
    private IPosSelectorUIPrinter posSelectorUIPrinter;

    [Inject]
    private CameraChangeAble cameraChanger;

    [Inject]
    private SelectPhazeCameraControllable cameraController;

    private OrthoCameraZoomAble zoomController;

    SelectedPawn pawn;

    private CancellationToken token;

    private PawnType selectType;

    [SerializeField]
    private float zoom;

    private bool onSelect;

    private PlayerInput input;

    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 movedir = context.ReadValue<Vector2>();
        cameraController.SetMoveDir(movedir);
    }

    public void End(int pawnID)
    {
        onSelect = false;
        input.SwitchCurrentActionMap("None");
        cameraChanger.ChangeToPawnCamera(pawnID);
        posSelectorUIPrinter.DestroyPosSelectorUI();
        cameraController.RangeMode = false;
        if (pawn != null) { pawn.SelectedUnFocus(); pawn = null; }
    }

    public async UniTask<int> PawnSelect(int pawnID, PawnType type)
    {
        BattleCmdSelectable p1 = strage.GetPawnByID<BattleCmdSelectable>(pawnID);

        Vector2 startPos = p1.VirtualPos;

        isConfirm = false;
        isCancel = false;

        selectType = type;
        posSelectorUIPrinter.PrintPosSelectorUI();

        cameraChanger.ChangeToSelectPhazeCamera();
        zoomController = cameraChanger.GetZoomController();

        zoomController.OrthoSize = zoom;

        cameraController.SetFirstPos(startPos);
        cameraController.Range = p1.AttackRange;
        cameraController.RangeMode = true;

        onSelect = true;

        input.SwitchCurrentActionMap("Battle");

        while (pawn == null)
        {
            await UniTask.WaitUntil(() => (isConfirm | isCancel), cancellationToken: token);
            if (isCancel) { End(pawnID); return -1; }
        }
        int ans = pawn.ID;
        End(pawnID);

        return ans;
    }

    private void RayHit(Collider2D collider)
    {
        Debug.Log(collider.gameObject.name);
        var obj = collider.gameObject.transform.root;
        var p2 = obj.GetComponent<SelectedPawn>();
        if (p2 == null || p2.Type != selectType) return;

        if(pawn != p2) p2.SelectedFocus();
        pawn = p2; 
    }



    // Use this for initialization
    void Start()
    {
        onSelect = false;
        token = this.GetCancellationTokenOnDestroy();
        input = GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!onSelect) return;

        Vector3 screenCenter = new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0.0f);
        Vector3 center = Camera.main.ScreenToWorldPoint(screenCenter);
        //center.y += 0.5f;

        RaycastHit2D hit = Physics2D.Raycast(center, Vector2.down, 0f);

        if (hit.collider != null) RayHit(hit.collider);
        else if (pawn != null)
        {
            pawn.SelectedUnFocus();
            pawn = null;
        }
    }
}