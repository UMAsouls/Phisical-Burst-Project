

using Cysharp.Threading.Tasks;
using UnityEngine;

public class LongRangeBehaviour : CommandBehaviourBase<ILongRangeAttackCommand>
{

    private Vector2 pos;

    public LongRangeBehaviour(ILongRangeAttackCommand cmd, bool burst, Vector2 pos)
    {
        this.cmd = cmd;
        this.burst = burst;
        this.pos = pos;
    }

    public override async UniTask DoAction(int pawnID)
    {
        throw new System.NotImplementedException();
    }
}
