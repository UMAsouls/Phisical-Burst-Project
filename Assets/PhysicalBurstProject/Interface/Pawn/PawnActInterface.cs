using Cysharp.Threading.Tasks;
using UnityEngine;

public interface PawnActInterface
{
    public UniTask EmergencyBattle();

    public void MoveAnimation(Vector2 dir);

    public void EndMove();
}
