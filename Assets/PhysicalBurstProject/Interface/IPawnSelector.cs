using Cysharp.Threading.Tasks;
using System.Collections;
using UnityEngine;
public interface IPawnSelector
{
    public UniTask<int> PawnSelect(Vector2 startPos, PawnType type);
}