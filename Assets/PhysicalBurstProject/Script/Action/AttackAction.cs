using UnityEngine;
using Cysharp.Threading.Tasks;

public abstract class AttackAction : IAction
{
    private IBattleCommand[] cmds;

    private AttackAble battlePawn;

    public ActionType Type => ActionType.Attack;

    protected abstract int ActPoint { get; }

    protected abstract int PriorityBonus { get; }

    protected abstract string actName {  get; }

    public  AttackAction(IBattleCommand[] cmds, AttackAble battlePawn)
    {
        this.cmds = cmds;
        this.battlePawn = battlePawn;
    }

    public bool CancelAct(ActionSettable pawn)
    {
        pawn.UseActPoint(-1*ActPoint);
        foreach (var cmd in cmds) { pawn.VirtualMana += cmd.UseMana; }
        return true;
    }

    public UniTask DoAct(ActablePawn pawn)
    {
        throw new System.NotImplementedException();
    }

    public string GetActionName()
    {
        return actName;
    }

    public bool setAct(ActionSettable pawn)
    {
        pawn.UseActPoint(ActPoint);

        pawn.ActionAdd(this);

        return true;
    }
}
