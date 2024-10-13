using System;
using System.Collections;
using UnityEngine;

public class BattleCommand : IBattleCommand
{
    private string name;
    public string Name => name;

    public BattleCommandType Type => throw new NotImplementedException();
}