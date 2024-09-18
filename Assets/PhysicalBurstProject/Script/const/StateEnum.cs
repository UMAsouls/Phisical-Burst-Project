using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleStateEventID
{
    TurnStart,
    SelectStart,
    ActionStart,
    BattleStart,
    BattleEnd,
    ActionEnd,
    End
}

public enum BattleStateID
{
    StartState,
    TurnStartState,
    SelectState,
    ActionState,
    BattleState,
    TurnEndState,
    EndState
}
