

using Cysharp.Threading.Tasks;

public interface CommandBehaviourMakeable
{
    public  UniTask<IActionCommandBehaviour> MakeCommandBehaviour(IActionCommand cmd, int pawnID);
}
