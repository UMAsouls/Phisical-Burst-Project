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

    protected override InputMode SelfMode => InputMode.Ambush;

    public async UniTask<bool> AmbushSelect(int pawnID)
    {
        inputModeBroker.BroadCast(InputModeTopic.SwitchActionMap, SelfMode);

        AmbushPawn pawn = strage.GetPawnByID<AmbushPawn>(pawnID);

        cameraChanger.ChangeToMovableCamera();
        cameraControler.Position = pawn.VirtualPos;

        var obj = Instantiate(RangeViewer, pawn.VirtualPos, Quaternion.identity);
        float attackRange = pawn.AttackRange;

        if (pawn.ActPoint >= 2) attackRange *= 1.2f;
        else attackRange *= 0.8f;

        Vector3 scale = Vector2.one * attackRange*2;
        scale.z = 1;
        obj.transform.localScale = scale;

        isConfirm = false;
        isCancel = false;
        
        await UniTask.WaitUntil(() => isConfirm || isCancel, cancellationToken: destroyCancellationToken);

        cameraChanger.ChangeToPawnCamera(pawnID);
        Destroy(obj);
        if (isCancel) return false;

        new AmbushAction(attackRange, pawn.ActPoint).setAct(strage.GetPawnByID<ActionSettable>(pawnID));
        return true;
    }

    // Use this for initialization

    // Update is called once per frame
    void Update()
    {

    }
}