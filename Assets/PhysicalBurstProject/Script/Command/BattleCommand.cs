using System;
using System.Collections;
using UnityEngine;

[Serializable]
public abstract class BattleCommand : IBattleCommand
{
    [SerializeField]
    protected string name;
    public string Name => name;

    [SerializeField]
    protected float mana;
    public float UseMana => mana;

    public abstract BattleCommandType Type { get; }
}