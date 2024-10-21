using Cysharp.Threading.Tasks;
using UnityEngine;


public class HasteAction : AttackAction
{
    protected override int ActPoint => 2;

    protected override int PriorityBonus => 1;

    protected override string actName => "速攻";

    public HasteAction(IBattleCommand[] cmds, AttackAble battlePawn) : base(cmds, battlePawn) { }
}
