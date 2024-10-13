using UnityEngine;

public class ActionMaker: MoveActionMakeable
{
    public IAction MakeMoveAction(Vector2 delta)
    {
        return new MoveAction(delta);
    }

}
