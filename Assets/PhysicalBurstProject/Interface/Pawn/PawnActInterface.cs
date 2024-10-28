using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

public interface PawnActInterface: AttackAble
{
    public new Vector2 Position { get; set; }
    public void MoveAnimation(Vector2 dir);

    public void EndMove();
    public float Size { get; }
}
