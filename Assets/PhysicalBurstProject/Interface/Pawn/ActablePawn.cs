using Cysharp.Threading.Tasks;
using UnityEngine;

public interface ActablePawn
{
    public UniTask movePos(Vector2 delta);
}
