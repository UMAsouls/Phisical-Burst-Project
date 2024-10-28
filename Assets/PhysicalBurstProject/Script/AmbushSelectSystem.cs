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

        AmbushPawn pawn = strage.GetPawnByID<AmbushPawn>(pawnID);

        cameraChanger.ChangeToMovableCamera();
        cameraControler.Position = pawn.VirtualPos;

        var obj = Instantiate(RangeViewer, pawn.VirtualPos, Quaternion.identity);
        float attackRange = pawn.AttackRange;

        if (pawn.ActPoint >= 2) attackRange *= 3f;
        else attackRange *= 2f;

        Vector3 scale = Vector2.one * attackRange;
        scale.z = 1;
        obj.transform.localScale = scale;

        isConfirm = false;
        isCancel = false;
        
        await UniTask.WaitUntil(() => isConfirm || isCancel, cancellationToken: destroyCancellationToken);

        input.SwitchCurrentActionMap("None");
        cameraChanger.ChangeToPawnCamera(pawnID);
        Destroy(obj);
        if (isCancel) return false;

        new AmbushAction(attackRange, pawn.ActPoint).setAct(strage.GetPawnByID<ActionSettable>(pawnID));
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