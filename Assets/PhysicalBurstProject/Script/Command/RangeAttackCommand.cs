using System.Collections;
using UnityEngine;

public class RangeAttackCommand : ActionCommand<IRangeAttackCommand>, IRangeAttackCommand
{
    public override ActionCmdType Type => ActionCmdType.RangeAttack;

    public float Range => throw new System.NotImplementedException();

    public float Damage => throw new System.NotImplementedException();
}