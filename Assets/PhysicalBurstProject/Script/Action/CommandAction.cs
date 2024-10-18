

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

    public UniTask DoAct(ActablePawn pawn)
    {
        throw new NotImplementedException();
    }

    public bool CancelAct(ActionSettable pawn)
    {
        pawn.VirtualMana += behaviour.UseMana;
        if (behaviour.IsBurst) pawn.VirtualHP += pawn.MaxHP / 5;

        return true;
    }

    public bool setAct(ActionSettable pawn)
    {
        if (!pawn.UseActPoint(1)) return false;

        pawn.VirtualMana -= behaviour.UseMana;
        if (behaviour.IsBurst) pawn.VirtualHP -= pawn.MaxHP / 5;
        pawn.ActionAdd(this);

        return true;
    }
}
