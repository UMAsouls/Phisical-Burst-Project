
using UnityEngine;

public interface IEnemyPawn: IDGettable, PosGetPawn
{
    public Vector2 VirtualPos { get; set; }
    public float Range { get; }
    public float AttackRange { get; }
}
