using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBattleCommand: ICommand
{
    public BattleCommandType Type { get; }
}
