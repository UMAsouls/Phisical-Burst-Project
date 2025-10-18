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

    public bool CancelAct(IPawnActionManager manager, IVirtualPawn vpawn, IStatus status)
    {
        manager.UseActPoint(-1*ActPoint);
        foreach (var cmd in cmds) { vpawn.VirtualMana += cmd.UseMana; }
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

    public bool setAct(IPawnActionManager manager, IVirtualPawn vpawn, IStatus status)
    {
        manager.UseActPoint(ActPoint);

        manager.ActionAdd(this);

        return true;
    }
}
