using System;
using System.Collections;
using UnityEngine;

public abstract class BattleCommand : IBattleCommand
{
    protected string name;
    public string Name => name;

    protected float mana;
    public float UseMana => mana;

    public abstract BattleCommandType Type { get; }

    private float burstRatio;
    public float BurstRatio => burstRatio;
}