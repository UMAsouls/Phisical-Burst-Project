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

    [SerializeField]
    [Range(1f, 20f)]
    public float DefencedBonus;

    public StrongAttackCommand()
    {
        this.damage = 0f;
        this.burstRatio = 0f;
        DefencedBonus = 0;
    }

    public StrongAttackCommand(BattleCommand cmd, float damage, float burstRatio, float defencedBonus) : base(cmd)
    {
        this.damage = damage;
        this.burstRatio = burstRatio;
        DefencedBonus = defencedBonus;
    }

    public StrongAttackCommand(StrongAttackCommand cmd) : this(cmd, cmd.damage, cmd.burstRatio, cmd.DefencedBonus) { }

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
        if (targetType == BattleCommandType.Weak && priority <= 0)
        {
            pawn.AttackEmote(target.Position - pawn.Position);
            await pawn.Crash();
            pawn.AttackEnd = true;
            return;
        }

        if(targetType == BattleCommandType.Defence && priority >= 0)
        {
            dmg *= DefencedBonus;
        }

        pawn.AttackEmote(target.Position - pawn.Position);
        bool avoid = !await target.Damage(dmg, pawn.ID);
        if (avoid) pawn.Priority -= 1;
        pawn.AttackEnd = true;

        Debug.Log("SAttackEnd");

    }

    public override string GetTypeText()
    {
        return "強攻撃";
    }

    public override IBattleCommand Copy() => new StrongAttackCommand(this);
}
