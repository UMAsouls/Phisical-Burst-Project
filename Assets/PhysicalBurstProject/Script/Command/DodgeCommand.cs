using Cysharp.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "DodgeCommand", menuName = "PBP/Command/Battle/DodgeCommand")]
public class DodgeCommand : BattleCommand
{
    public override BattleCommandType Type => BattleCommandType.Dodge;

    [SerializeField]
    [Range(0f, 100f)]
    private float possibility;

    [SerializeField]
    [Range(0, 5)]
    private int Bonus;

    public DodgeCommand()
    {
        possibility = 0f;
        Bonus = 0;
    }

    public DodgeCommand(BattleCommand cmd, float possibility, int bonus): base(cmd)
    {
        this.possibility = possibility;
        Bonus = bonus;
    }

    public DodgeCommand(DodgeCommand cmd) : this(cmd, cmd.possibility, cmd.Bonus) { }

    public override async UniTask Do(AttackAble pawn, AttackAble target, BattleCommandType targetType)
    {
        var p = possibility;

        var priority = pawn.Priority-target.Priority;

        if (targetType == BattleCommandType.Strong || (pawn.Burst && targetType != BattleCommandType.Weak)) p = 100;
        else if (priority <= -2) p *= 0.3f;
        else if (priority <= -1) p *= 0.6f;

        if (target.Burst && !pawn.Burst) p = 0;
        else if (target.Burst && pawn.Burst && targetType == BattleCommandType.Strong)
        {
            p = Mathf.Clamp(possibility*2f, 0, 100);
            if (priority <= -2) p *= 0.5f;
            else if (priority <= -1) p *= 0.7f;
        }

        bool avoid = Random.Range(1, 100) <= p;
        pawn.Avoid = avoid;

        pawn.DamageAble = true;
        pawn.AttackEnd = true;

        Debug.Log("DodgeEnd");

        bool attacked = targetType == BattleCommandType.Strong || targetType == BattleCommandType.Weak;

        await UniTask.WaitUntil(() => target.AttackEnd);

        if (!avoid && attacked) pawn.Priority -= 1;
        else if(attacked) pawn.Priority += Bonus;

    }

    public override string GetTypeText()
    {
        return "回避";
    }

    public override IBattleCommand Copy() => new DodgeCommand(this);
}
