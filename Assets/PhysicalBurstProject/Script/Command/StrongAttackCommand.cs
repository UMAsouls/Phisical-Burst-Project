using System;
using UnityEngine;

[Serializable]
public class StrongAttackCommand : BattleCommand
{

    public override BattleCommandType Type => BattleCommandType.Strong;

    [SerializeField]
    public float damage;

    [SerializeField]
    public float burstRatio;
}
