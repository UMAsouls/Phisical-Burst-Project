using System;
using UnityEngine;

[Serializable]
public class WeakAttackCommand : BattleCommand
{
    public override BattleCommandType Type =>BattleCommandType.Weak;

    [SerializeField]
    private float damage;

    [SerializeField]
    private float burstRatio;
}
