using Cysharp.Threading.Tasks;
using UnityEngine;

public interface IRangeAttackCommand : IEasyEffectCommand
{
    public float Range { get; }

    public float Damage { get; }
}
