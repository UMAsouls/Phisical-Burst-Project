using Cysharp.Threading.Tasks;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class AmbushSelectSystem : ConfirmCancelCatchAble
{

    [Inject]
    IPawnGettable strage;

    [Inject]
    CameraChangeAble cameraChanger;

    [Inject]
    CameraControllable cameraControler;

    [SerializeField]
    private GameObject RangeViewer;

    private PlayerInput input;

    public async UniTask<bool> AmbushSelect(int pawnID)
    {
        input.SwitchCurrentActionMap("Ambush");

        var pawn = strage.GetPawnComponentByID<IBattlePawn>(pawnID);
        var actManager = pawn.ActionManager;
        var status = pawn.Status;
        var vpawn = pawn.VirtualPawn;

        cameraChanger.ChangeToMovableCamera();
        cameraControler.Position = vpawn.VirtualPos;

        var obj = Instantiate(RangeViewer, vpawn.VirtualPos, Quaternion.identity);
        float attackRange = status.AttackRange;

        if (actManager.ActPoint >= 2) attackRange *= 1.2f;
        else attackRange *= 0.8f;

        Vector3 scale = Vector2.one * attackRange*2;
        scale.z = 1;
        obj.transform.localScale = scale;

        isConfirm = false;
        isCancel = false;
        
        await UniTask.WaitUntil(() => isConfirm || isCancel, cancellationToken: destroyCancellationToken);

        input.SwitchCurrentActionMap("None");
        cameraChanger.ChangeToPawnCamera(pawnID);
        Destroy(obj);
        if (isCancel) return false;

        new AmbushAction(attackRange, actManager.ActPoint).setAct(actManager, vpawn, status);
        return true;
    }

    // Use this for initialization
    void Start()
    {
        input = GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}