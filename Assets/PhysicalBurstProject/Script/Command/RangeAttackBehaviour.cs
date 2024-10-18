
using Cysharp.Threading.Tasks;

public class RangeAttackBehaviour : CommandBehaviourBase<IRangeAttackCommand>
{

    public RangeAttackBehaviour(IRangeAttackCommand cmd, bool burst)
    {
        this.cmd = cmd;
        this.burst = burst;
    }

    public override async UniTask DoAction(int pawnID)
    {
        throw new System.NotImplementedException();
    }
}
