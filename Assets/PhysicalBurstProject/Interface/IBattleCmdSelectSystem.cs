using Cysharp.Threading.Tasks;

public interface IBattleCmdSelectSystem
{
    public UniTask<IBattleCommand[]> Select(int pawnID);
}
