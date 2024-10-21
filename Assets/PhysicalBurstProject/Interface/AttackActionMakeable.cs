using UnityEngine;

public interface AttackActionMakeable
{
    public IAction MakeHasteAction(IBattleCommand[] cmds, AttackAble pawn);
    public IAction MakeNormalAttackAction(IBattleCommand[] cmds, AttackAble pawn);
}