using Cysharp.Threading.Tasks;
using System.Collections;
using UnityEngine;
using Zenject;
using System.Threading;
using UnityEngine.InputSystem;

public class PawnSelector : ConfirmCancelCatchAble, IPawnSelector
{

    [Inject]
    private IPosSelectorUIPrinter posSelectorUIPrinter;

    [Inject]
    private CameraChangeAble cameraChanger;

    [Inject]
    private SelectPhazeCameraControllable cameraController;

    private OrthoCameraZoomAble zoomController;

    Ray ray;

    SelectedPawn pawn;

    private CancellationToken token;

    private PawnType selectType;

    private Vector2 cameraPos;

    private Vector2 movedir;
    [SerializeField]
    private float moveSpeed;

    [SerializeField]
    private float zoom;

    private bool onSelect;

    public void OnMove(InputAction.CallbackContext context)
    {
        movedir = context.ReadValue<Vector2>();
    }

    public async UniTask<int> PawnSelect(Vector2 startPos, PawnType type)
    {

        selectType = type;
        posSelectorUIPrinter.PrintPosSelectorUI();

        cameraChanger.ChangeToSelectPhazeCamera();
        zoomController = cameraChanger.GetZoomController();

        zoomController.OrthoSize = zoom;

        cameraPos = startPos;
        cameraController.Position = cameraPos;

        onSelect = true;

        while (pawn == null)
        {
            await UniTask.WaitUntil(() => (isConfirm | isCancel), cancellationToken: token);
            if (isCancel) { onSelect = false; return -1; }
        }

        onSelect = false;

        return pawn.ID;
    }

    private void RayHit(Collider2D collider)
    {
        var obj = collider.gameObject;
        pawn = obj.GetComponent<SelectedPawn>();
        if (pawn == null || pawn.Type != selectType) return;

        pawn.SelectedFocus();
    }



    // Use this for initialization
    void Start()
    {
        onSelect = false;
        token = this.GetCancellationTokenOnDestroy();
        cameraPos = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if(!onSelect) return;

        Vector3 screenCenter = new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0.0f);
        RaycastHit2D hit = Physics2D.Raycast(screenCenter, Vector2.zero);

        if (hit.collider != null) RayHit(hit.collider);
        else
        {
            pawn = null;
            pawn.SelectedUnFocus();
        }

        cameraPos += movedir;
        cameraController.Position = cameraPos;
    }
}