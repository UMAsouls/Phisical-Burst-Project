using Cysharp.Threading.Tasks;
using UnityEngine;


public class HasteAction : IAction
{
    public IBattleCommand[] cmds;

    public AttackAble battlePawn;

    public ActionType Type => ActionType.Attack;

    public bool CancelAct(ActionSettable pawn)
    {
        pawn.UseActPoint(-2);
        foreach (var cmd in cmds) { pawn.VirtualMana += cmd.UseMana; }
        return true;
    }

    public UniTask DoAct(ActablePawn pawn)
    {
        throw new System.NotImplementedException();
    }

    public string GetActionName()
    {
        return "速攻";
    }

    public bool setAct(ActionSettable pawn)
    {
        pawn.ActionAdd(this);

        return true;
    }
}
