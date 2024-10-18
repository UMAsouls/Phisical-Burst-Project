

using Cysharp.Threading.Tasks;

public class HealBehaviour : CommandBehaviourBase<IHealCommand>
{

    public HealBehaviour(IHealCommand cmd, bool burst)
    {
        this.cmd = cmd;
        this.burst = burst;
    }
    
    public override async UniTask DoAction(int pawnID)
    {
        throw new System.NotImplementedException();
    }
}
