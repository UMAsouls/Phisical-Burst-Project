using System.Collections;
using UnityEngine;

public interface IHealCommand: IActionCommand
{
    public float Range { get; }

    public float Heal { get; }
}