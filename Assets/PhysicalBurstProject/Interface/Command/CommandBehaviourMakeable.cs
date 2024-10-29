

using Cysharp.Threading.Tasks;

public interface CommandBehaviourMakeable
{
    public  UniTask<IActionCommandBehaviour> MakeCommandBehaviour(IActionCommand cmd, int pawnID);
    public IActionCommandBehaviour MakeRangeBehaviour(IActionCommand cmd, bool burst, PawnType target);
}
