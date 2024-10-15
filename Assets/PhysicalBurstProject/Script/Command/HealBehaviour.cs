

using Cysharp.Threading.Tasks;

public class HealBehaviour : IActionCommandBehaviour
{
    private IHealCommand cmd;

    public HealBehaviour(IHealCommand cmd)
    {
        this.cmd = cmd;
    }
    
    public UniTask DoAction(int pawnID)
    {
        throw new System.NotImplementedException();
    }
}
