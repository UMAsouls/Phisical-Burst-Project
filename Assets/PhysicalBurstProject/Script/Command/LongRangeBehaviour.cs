

using Cysharp.Threading.Tasks;
using UnityEngine;

public class LongRangeBehaviour : IActionCommandBehaviour
{
    private ILongRangeAttackCommand cmd;

    private Vector2 pos;

    public LongRangeBehaviour(ILongRangeAttackCommand cmd, Vector2 pos)
    {
        this.cmd = cmd;
        this.pos = pos;
    }

    public UniTask DoAction(int pawnID)
    {
        throw new System.NotImplementedException();
    }
}
