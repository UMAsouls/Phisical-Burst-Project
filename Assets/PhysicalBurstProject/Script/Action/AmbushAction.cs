using Cysharp.Threading.Tasks;
using System.Security.Cryptography;
using UnityEngine;

public class AmbushAction : IAction
{
    public ActionType Type => ActionType.Ambush;

    private float range;

    private int actPoint;

    public AmbushAction(float range, int actPoint)
    {
        this.range = range;
        this.actPoint = actPoint;
    }

    public bool CancelAct(ActionSettable pawn)
    {
        pawn.UseActPoint(-1*actPoint);
        return true;
    }

    public async UniTask DoAct(ActablePawn pawn)
    {
        pawn.Priority += 1;
        await pawn.Ambush(range);
        pawn.Priority -= 1;
    }

    public string GetActionName()
    {
        return "待ち伏せ";
    }

    public bool setAct(ActionSettable pawn)
    {
        pawn.UseActPoint(actPoint);

        pawn.ActionAdd(this);
        return true;
    }
}
