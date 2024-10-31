using UnityEngine;
using Cysharp.Threading.Tasks;

public abstract class AttackAction : IAction
{
    private IBattleCommand[] cmds;

    private AttackAble target;

    public ActionType Type => ActionType.Attack;

    protected abstract int ActPoint { get; }

    protected abstract int PriorityBonus { get; }

    protected abstract string actName {  get; }

    public  AttackAction(IBattleCommand[] cmds, AttackAble target)
    {
        this.cmds = cmds;
        this.target = target;
    }

    public bool CancelAct(ActionSettable pawn)
    {
        pawn.UseActPoint(-1*ActPoint);
        foreach (var cmd in cmds) { pawn.VirtualMana += cmd.UseMana; }
        return true;
    }

    public virtual async UniTask DoAct(ActablePawn pawn)
    {
        pawn.Priority += PriorityBonus;
        await pawn.Battle(cmds, target);
        pawn.Priority -= PriorityBonus;
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
