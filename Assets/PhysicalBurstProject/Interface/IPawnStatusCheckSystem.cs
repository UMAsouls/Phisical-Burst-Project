using UnityEngine;
using Cysharp.Threading.Tasks;

public interface IPawnStatusCheckSystem
{
    public UniTask PawnStatusCheck(int id);
}
