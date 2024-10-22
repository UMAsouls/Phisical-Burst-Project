using UnityEngine;

public interface IPawnAnimator
{
    public void MoveAnimation(Vector2 dir);

    public void EndMove();
}
