using System;
using System.Collections;
using UnityEngine;

public class BattleCommand : IBattleCommand
{
    protected string name;
    public string Name => name;

    protected float mana;
    public float UseMana => mana;

    public BattleCommandType Type => throw new NotImplementedException();
}