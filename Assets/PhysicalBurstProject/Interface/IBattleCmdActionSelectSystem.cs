

using Cysharp.Threading.Tasks;

public interface IBattleCmdActionSelectSystem
{

    public UniTask<bool> Select(int pawnID);
}
