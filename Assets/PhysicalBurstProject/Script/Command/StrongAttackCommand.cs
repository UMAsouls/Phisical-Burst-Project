using System;
using UnityEngine;

[Serializable]
public class StrongAttackCommand : BattleCommand
{

    public override BattleCommandType Type => BattleCommandType.Strong;

    [SerializeField]
    public float damage;

    [SerializeField]
    [Range(0f, 10f)]
    public float burstRatio;
}
