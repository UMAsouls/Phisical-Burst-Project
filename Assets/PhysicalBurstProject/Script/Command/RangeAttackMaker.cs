
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.PlayerSettings;

public class RangeAttackMaker : CommandMakerBase<IRangeAttackCommand>
{

    [SerializeField]
    private GameObject RangeViewer;

    private CancellationToken cts;

    public override async UniTask<IActionCommandBehaviour> MakeBehaviour(IRangeAttackCommand cmd, int pawnID)
    {
        cameraZoomController.OrthoSize = cmd.Range;

        var vpawn = strage.GetPawnByID<IVirtualPawn>(pawnID);
        var obj = Instantiate(RangeViewer, (Vector3)(vpawn.VirtualPos), Quaternion.identity);

        var r_scaler = obj.GetComponent<IRangeCircleScaler>();
        r_scaler.SetRadius(cmd.Range);

        await UniTask.WaitUntil(() => (isCancel || isConfirm), PlayerLoopTiming.Update, cts);

        Destroy(obj);
        var behaviour = new RangeAttackBehaviour(cmd, isBurst, PawnType.Enemy);
        container.Inject(behaviour);
        if (isConfirm) { return behaviour; }
        else { return null; }
    }

    protected override void Awake()
    {
        base.Awake();
        actionMap = "Range";
        cts = this.GetCancellationTokenOnDestroy();
    }
}
