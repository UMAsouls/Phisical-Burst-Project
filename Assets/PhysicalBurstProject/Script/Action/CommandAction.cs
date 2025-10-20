

using Cysharp.Threading.Tasks;
using System;

public class CommandAction : IAction
{
    public ActionType Type => ActionType.Action;

    private IActionCommandBehaviour behaviour;

    public CommandAction(IActionCommandBehaviour behaviour)
    {
        this.behaviour = behaviour;
    }

    public async UniTask DoAct(IActionUnit actUnit)
    {
        await actUnit.Action(behaviour);
    }

    public bool CancelAct(IPawnActionManager manager, IVirtualPawn vpawn, IStatus status)
    {
        vpawn.VirtualMana += behaviour.UseMana;
        if (behaviour.IsBurst) vpawn.VirtualHP += status.MaxHP / 5;

        manager.UseActPoint(-1);
        return true;
    }

    public bool setAct(IPawnActionManager manager, IVirtualPawn vpawn, IStatus status)
    {
        if (!manager.UseActPoint(1)) return false;

        vpawn.VirtualMana -= behaviour.UseMana;
        if (behaviour.IsBurst) vpawn.VirtualHP -= status.MaxHP / 5;
        behaviour.SetCommand(manager.ID);
        manager.ActionAdd(this);

        return true;
    }

    public string GetActionName()
    {
        return "行動: " + behaviour.Name;
    }
}
