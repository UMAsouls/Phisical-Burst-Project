using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

public interface PawnActInterface: AttackAble
{

    public void MoveAnimation(Vector2 dir);

    public void EndMove();
}
