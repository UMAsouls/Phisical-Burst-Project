using System.Collections;
using UnityEngine;

public class RangeAttackCommand : ActionCommand<IRangeAttackCommand>, IRangeAttackCommand
{
    public override ActionCmdType Type => ActionCmdType.RangeAttack;

    private float range;
    public float Range => range;

    private float damage;
    public float Damage => damage;
}