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
        var dmg = damage*(pawn.attack/2);
        if(pawn.Burst) dmg *= burstRatio;

        pawn.DamageAble = true;

        var priority = pawn.Priority - target.Priority;

        if (targetType == BattleCommandType.Strong) return;
        if(targetType == BattleCommandType.Weak && priority >= 0) return;

        await target.Damage(dmg, DamageType.Strong);
        
    }
}
