using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class BattleCommand : ICommand
{
    private string name;

    public string Name => name;

    
}