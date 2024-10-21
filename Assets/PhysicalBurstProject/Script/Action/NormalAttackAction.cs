using UnityEngine;

public class NormalAttackAction : AttackAction
{
    protected override int ActPoint => 1;

    protected override int PriorityBonus => 0;

    protected override string actName => "襲撃";

    public NormalAttackAction(IBattleCommand[] cmds, AttackAble pawn) : base(cmds, pawn) { }
}
