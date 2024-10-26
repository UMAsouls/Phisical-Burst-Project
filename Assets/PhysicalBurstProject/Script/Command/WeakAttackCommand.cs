using Cysharp.Threading.Tasks;
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

    public override UniTask Do(AttackAble pawn, AttackAble target, BattleCommandType targetType)
    {
        throw new NotImplementedException();
    }
}
