using UnityEngine;

public interface IPawnAnimator
{
    public void MoveAnimation(Vector2 dir);

    public void EndMove();

    public void ChangeNormal();

    public void ChangeStun();

    public void ChangeBurst();
}
