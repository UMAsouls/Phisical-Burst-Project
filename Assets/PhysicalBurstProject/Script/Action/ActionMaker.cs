using UnityEngine;

public class ActionMaker: MoveActionMakeable, CommandActionMakeable, AttackActionMakeable
{
    public IAction MakeMoveAction(Vector2 delta)
    {
        return new MoveAction(delta);
    }

    public IAction MakeCommandAction(IActionCommandBehaviour behaviour)
    {
        return new CommandAction(behaviour);
    }

    public IAction MakeHasteAction(IBattleCommand[] cmds, AttackAble pawn)
    {
        return new HasteAction(cmds, pawn);
    }

    public IAction MakeAmbushAction(float range, int actPoint)
    {
        return new AmbushAction(range, actPoint);
    }

    public IAction MakeNormalAttackAction(IBattleCommand[] cmds, AttackAble pawn)
    {
        return new NormalAttackAction(cmds, pawn);
    }
}
