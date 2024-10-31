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

    public WeakAttackCommand(BattleCommand cmd, float damage, float burstRatio) : base(cmd)
    {
        this.damage = damage;
        this.burstRatio = burstRatio;
    }

    public WeakAttackCommand(WeakAttackCommand cmd) : this(cmd, cmd.damage, cmd.burstRatio) { }

    public override async UniTask Do(AttackAble pawn, AttackAble target, BattleCommandType targetType)
    {
        var dmg = damage * (pawn.attack / 5.5f);
        if (pawn.Burst) dmg *= burstRatio;

        var priority = pawn.Priority - target.Priority;

        pawn.DamageAble = true;

        if (targetType == BattleCommandType.Strong)
        {
            if (priority >= -1)
            {
                if(priority >= 0)
                {
                    pawn.AttackEmote(target.Position - pawn.Position);
                    await pawn.Crash();
                }
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
        if (targetType == BattleCommandType.Weak)
        {
            if (priority <= 0)
            {
                if (priority == 0)
                {
                    pawn.AttackEmote(target.Position - pawn.Position);
                    await pawn.Crash();
                }
                pawn.AttackEnd = true;
                return;
            }
        }

        pawn.AttackEmote(target.Position - pawn.Position);
        bool avoid = !await target.Damage(dmg, pawn.ID);
        if (avoid) pawn.Priority -= 0;
        pawn.AttackEnd = true;

        Debug.Log("AttackEnd");
    }

    public override string GetTypeText()
    {
        return "弱攻撃";
    }

    public override IBattleCommand Copy() => new WeakAttackCommand(this);
}
