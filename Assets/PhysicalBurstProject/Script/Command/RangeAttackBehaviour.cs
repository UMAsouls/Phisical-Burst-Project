
using Cysharp.Threading.Tasks;

public class RangeAttackBehaviour : IActionCommandBehaviour
{

    private IRangeAttackCommand cmd;

    public RangeAttackBehaviour(IRangeAttackCommand cmd)
    {
        this.cmd = cmd;
    }

    public async UniTask DoAction(int pawnID)
    {
        throw new System.NotImplementedException();
    }
}
