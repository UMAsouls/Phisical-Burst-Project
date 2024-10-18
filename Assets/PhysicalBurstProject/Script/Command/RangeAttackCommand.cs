using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class RangeAttackCommand : ActionCommand<IRangeAttackCommand>, IRangeAttackCommand
{
    public override ActionCmdType Type => ActionCmdType.RangeAttack;

    [SerializeField]
    private float range;
    public float Range => range;

    [SerializeField]
    private float damage;
    public float Damage => damage;
}