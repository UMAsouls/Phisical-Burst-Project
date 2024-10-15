using UnityEngine;
using UnityEditor;
using System;

[Serializable]
public class LongRangeAttackCommand : ActionCommand<ILongRangeAttackCommand>, ILongRangeAttackCommand
{
    private float attackArea;
    public float AttackArea => attackArea;

    private float range;
    public float Range => range;

    private float damage;
    public float Damage => damage;

    public override ActionCmdType Type => ActionCmdType.LongRangeAttack;
}
