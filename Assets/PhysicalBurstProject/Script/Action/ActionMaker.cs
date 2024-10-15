﻿using UnityEngine;

public class ActionMaker: MoveActionMakeable, CommandActionMakeable
{
    public IAction MakeMoveAction(Vector2 delta)
    {
        return new MoveAction(delta);
    }

    public IAction MakeCommandAction(IActionCommandBehaviour behaviour)
    {
        return new CommandAction(behaviour);
    }

}
