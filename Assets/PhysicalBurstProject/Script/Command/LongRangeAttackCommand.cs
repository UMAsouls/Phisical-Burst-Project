using UnityEngine;
using UnityEditor;
using System;

[Serializable]
public class LongRangeAttackCommand : ActionCommand<ILongRangeAttackCommand>, ILongRangeAttackCommand
{
    [SerializeField]
    private float attackArea;
    public float AttackArea => attackArea;

    [SerializeField]
    private float range;
    public float Range => range;

    [SerializeField]
    private float damage;
    public float Damage => damage;

    public override ActionCmdType Type => ActionCmdType.LongRangeAttack;
}
