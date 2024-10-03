using UnityEngine;

public interface MoveActionMakeable
{
    public IAction MakeMoveAction(Vector2 delta);
}
