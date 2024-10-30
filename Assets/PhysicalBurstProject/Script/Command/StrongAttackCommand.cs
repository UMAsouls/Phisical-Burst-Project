using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

[Serializable]
public class StrongAttackCommand : BattleCommand
{

    public override BattleCommandType Type => BattleCommandType.Strong;

    [SerializeField]
    public float damage;

    [SerializeField]
    [Range(0f, 10f)]
    public float burstRatio;

    public override async UniTask Do(AttackAble pawn, AttackAble target, BattleCommandType targetType)
    {
        var dmg = damage*(pawn.attack/5);
        if(pawn.Burst) dmg *= burstRatio;

        var priority = pawn.Priority - target.Priority;

        pawn.DamageAble = true;

        if (targetType == BattleCommandType.Strong)
        {
            if(priority <= 0)
            {
                if(priority == 0)
                {
                    pawn.AttackEmote(target.Position - pawn.Position);
                    await pawn.Crash();
                }
                pawn.AttackEnd = true;
                return;
            }
        }
        if (targetType == BattleCommandType.Weak && priority <= -1)
        {
            pawn.AttackEmote(target.Position - pawn.Position);
            await pawn.Crash();
            pawn.AttackEnd = true;
            return;
        }

        pawn.AttackEmote(target.Position - pawn.Position);
        bool avoid = !await target.Damage(dmg);
        if (avoid) pawn.Priority -= 1;
        pawn.AttackEnd = true;

        Debug.Log("SAttackEnd");

    }
}
