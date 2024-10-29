using Cysharp.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;


public class DodgeCommand : BattleCommand
{
    public override BattleCommandType Type => BattleCommandType.Dodge;

    [SerializeField]
    [Range(0f, 100f)]
    private float possibility;

    [SerializeField]
    [Range(0, 5)]
    private int Bonus;

    public override async UniTask Do(AttackAble pawn, AttackAble target, BattleCommandType targetType)
    {
        var p = possibility;

        var priority = pawn.Priority-target.Priority;

        if (targetType == BattleCommandType.Strong || pawn.Burst) p = 100;
        else if (priority <= -2) p *= 0.3f;
        else if (priority <= -1) p *= 0.6f;

        if (target.Burst && !pawn.Burst) p = 0;

        bool avoid = Random.Range(1, 100) <= p;
        pawn.Avoid = avoid;

        pawn.DamageAble = true;
        pawn.AttackEnd = true;

        Debug.Log("DodgeEnd");

        await UniTask.WaitUntil(() => target.AttackEnd);

        if (!avoid) pawn.Priority -= 1;
        else pawn.DodgeEffect(target.Position - pawn.Position);

    }
}
