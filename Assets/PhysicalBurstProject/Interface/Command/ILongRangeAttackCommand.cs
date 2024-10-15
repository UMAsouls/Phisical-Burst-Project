using System.Collections;
using UnityEngine;

public interface ILongRangeAttackCommand: IActionCommand
{
    public float AttackArea { get; }
    public float Range { get; }

    public float Damage { get; }
}