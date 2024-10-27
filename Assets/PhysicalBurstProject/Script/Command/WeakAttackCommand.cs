using Cysharp.Threading.Tasks;
using System;
using System.Security.Cryptography;
using UnityEngine;

[Serializable]
public class WeakAttackCommand : BattleCommand
{
    public override BattleCommandType Type =>BattleCommandType.Weak;

    [SerializeField]
    private float damage;

    [SerializeField]
    [Range(0f, 10f)]
    private float burstRatio;

    public override async UniTask Do(AttackAble pawn, AttackAble target, BattleCommandType targetType)
    {
        var dmg = damage * (pawn.attack / 7);
        if (pawn.Burst) dmg *= burstRatio;

        var priority = pawn.Priority - target.Priority;

        pawn.DamageAble = true;

        if (targetType == BattleCommandType.Strong)
        {
            if (priority >= 0)
            {
                pawn.AttackEnd = true;
                return;
            }
            else
            {
                await UniTask.WaitUntil(() => target.AttackEnd);
                pawn.AttackEnd = true;
                pawn.Stun();
                return;
            }
        }
        if (targetType == BattleCommandType.Weak) return;

        bool avoid = !await target.Damage(dmg);
        if (avoid) pawn.Priority -= 1;
        pawn.AttackEnd = true;

        Debug.Log("AttackEnd");
    }
}
