using System.Collections;
using UnityEngine;

public interface BattleCmdSelectable : ActionSettable
{
    public IBattleCommand[] BattleCommands { get; }
}