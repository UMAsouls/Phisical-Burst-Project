using System;
using UnityEngine;

[Serializable]
public class WeakAttackCommand : BattleCommand
{
    public override BattleCommandType Type =>BattleCommandType.Weak;

    [SerializeField]
    private float damage;

    [SerializeField]
    [Range(0f, 10f)]
    private float burstRatio;
}
