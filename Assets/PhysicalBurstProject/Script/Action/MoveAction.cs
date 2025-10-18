using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

public class MoveAction : IAction
{
    public MoveAction(Vector2 delta)
    {
        this.delta = delta;
    }

    public ActionType Type => ActionType.Move;

    private Vector2 delta;

    public async UniTask DoAct(ActablePawn pawn)
    {
        Debug.Log("actstart");
        await pawn.MovePos(delta);
    }

    public bool setAct(IPawnActionManager manager, IVirtualPawn vpawn, IStatus status)
    {
       if (!manager.UseActPoint(1)) return false;

        vpawn.VirtualPos += delta;
        manager.ActionAdd(this);

       return true;
    }

    public bool CancelAct(IPawnActionManager manager, IVirtualPawn vpawn, IStatus status)
    {
        vpawn.VirtualPos -= delta;
        manager.UseActPoint(-1);

        return true;
    }

    public string GetActionName()
    {
        return "移動";
    }
}
