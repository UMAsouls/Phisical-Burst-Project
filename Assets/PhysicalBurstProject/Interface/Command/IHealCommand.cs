using System.Collections;
using UnityEngine;

public interface IHealCommand: IEasyEffectCommand
{
    public float Range { get; }

    public float Heal { get; }
}