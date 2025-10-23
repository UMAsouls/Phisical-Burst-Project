using Cysharp.Threading.Tasks;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

public class LongRangeMaker : CommandMakerBase<ILongRangeAttackCommand>
{
    private Vector2 pos;

    [SerializeField]
    private GameObject AreaViewer;
    private GameObject area;

    private RangeMovable areaMover;

    [SerializeField]
    private GameObject RangeCircle;

    private CancellationToken cts;

    protected override InputMode SelfMode => InputMode.Action_LongRange;

    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 dir = context.ReadValue<Vector2>();
        areaMover.SetMoveDir(dir);
    }

    public override async UniTask<IActionCommandBehaviour> MakeBehaviour(ILongRangeAttackCommand cmd, int pawnID)
    {
        var vpawn = strage.GetPawnByID<IVirtualPawn>(pawnID);
        area = Instantiate(AreaViewer, (Vector3)(vpawn.VirtualPos), Quaternion.identity);
        var obj = Instantiate(RangeCircle, (Vector3)(vpawn.VirtualPos), Quaternion.identity);

        var a_scaler = area.GetComponent<IRangeCircleScaler>();
        a_scaler.SetRadius(cmd.AttackArea);

        var r_scaler = obj.GetComponent<IRangeCircleScaler>();
        r_scaler.SetRadius(cmd.Range);

        areaMover = area.GetComponent<RangeMovable>();
        areaMover.Range = cmd.Range;

        if(cameraZoomController.OrthoSize < cmd.Range) cameraZoomController.OrthoSize = cmd.Range;

        await UniTask.WaitUntil(() => (isCancel || isConfirm), PlayerLoopTiming.Update, cts);

        pos = area.transform.position;
        Destroy(area);
        Destroy(obj);
        var behaviour = new LongRangeBehaviour(cmd, isBurst, pos, PawnType.Enemy);
        container.Inject(behaviour);
        if (isConfirm) { return behaviour; }
        else { return null; }
    }

    protected override void Awake()
    {
        base.Awake();
        actionMap = "Long Range";

        cts = this.GetCancellationTokenOnDestroy();
    }

    public override void Start()
    {
        var moveAction = new ActionSetMessage(SelfMode, "Move", OnMove);
        actionSetBroker.BroadCast(ActionSetTopic.SetAction, moveAction);
        base.Start();
    }
}