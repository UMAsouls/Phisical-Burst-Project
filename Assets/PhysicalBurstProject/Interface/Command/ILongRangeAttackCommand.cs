using Cysharp.Threading.Tasks;
using System.Collections;
using UnityEngine;

public interface ILongRangeAttackCommand: IEasyEffectCommand
{
    public float AttackArea { get; }
    public float Range { get; }

    public float Damage { get; }

}