using UnityEngine;

public interface AttackAble: IDGettable, PawnTypeGettable
{
    public int HP { get; }

    public Vector2 Position { get; }

    public void Attack(int attack);

    public bool IsMove { get; }
}
