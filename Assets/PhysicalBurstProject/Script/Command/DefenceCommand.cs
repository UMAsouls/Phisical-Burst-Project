using Cysharp.Threading.Tasks;
using UnityEngine;

public class DefenceCommand : BattleCommand
{
    public override BattleCommandType Type => BattleCommandType.Defence;

    [SerializeField]
    [Range(0f, 100f)]
    private float ratio;

    [SerializeField]
    [Range(0f, 100f)]
    private float secondRatio;

    [SerializeField]
    [Range(0f, 100f)]
    private float thirdRatio;

    [SerializeField]
    [Range(0f, 100f)]
    private float burstRatio;

    [SerializeField]
    private bool Breakable = true;

    public DefenceCommand(BattleCommand cmd, float ratio, float secondRatio, float thirdRatio, float burstRatio, bool breakable): base(cmd)
    {
        this.ratio = ratio;
        this.secondRatio = secondRatio;
        this.thirdRatio = thirdRatio;
        this.burstRatio = burstRatio;
        Breakable = breakable;
    }

    public DefenceCommand(DefenceCommand cmd) : this(cmd, cmd.ratio, cmd.secondRatio, cmd.thirdRatio, cmd.burstRatio, cmd.Breakable) { }

    public override async UniTask Do(AttackAble pawn, AttackAble target, BattleCommandType targetType)
    {
        var guard = ratio;

        var priority = pawn.Priority - target.Priority;

        if(targetType == BattleCommandType.Strong)
        {
            if (priority <= -2 && !Breakable) guard = 0;
            else if (priority <= -1) guard = secondRatio;
            else if (priority <= 0) guard = thirdRatio;
        }

        if (pawn.Burst) { guard = burstRatio; }

        pawn.Guard = guard;
        pawn.DamageAble = true;
        pawn.AttackEnd = true;

        Debug.Log("DefenceEnd: " + pawn.AttackEnd);

        await UniTask.WaitUntil(() => target.AttackEnd);

        if(guard == 0) { pawn.Stun(); }

    }

    public override string GetTypeText()
    {
        return "防御";
    }

    public override IBattleCommand Copy() => new DefenceCommand(this);
}
