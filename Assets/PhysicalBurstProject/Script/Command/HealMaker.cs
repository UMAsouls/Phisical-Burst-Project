using Cysharp.Threading.Tasks;
using System.Collections;
using System.Threading;
using UnityEngine;

public class HealMaker : CommandMakerBase<IHealCommand>
{

    [SerializeField]
    private GameObject RangeCircle;

    private CancellationToken cts;

    public override async UniTask<IActionCommandBehaviour> MakeBehaviour(IHealCommand cmd , int pawnID)
    {
        var vpawn = strage.GetPawnById<IVirtualPawn>(pawnID);
        var obj = Instantiate(RangeCircle, (Vector3)(vpawn.VirtualPos), Quaternion.identity);

        var r_scaler = obj.GetComponent<IRangeCircleScaler>();
        r_scaler.SetRadius(cmd.Range);

        await UniTask.WaitUntil(() => (isCancel || isConfirm), PlayerLoopTiming.Update, cts);

        Destroy(obj);
        if (isConfirm) { return new HealBehaviour(cmd, isBurst); }
        else { return null; }
    }

    protected override void Awake()
    {
        base.Awake();
        actionMap = "Heal";

        cts = this.GetCancellationTokenOnDestroy();
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}