using UnityEngine;

public interface IPawnAnimator: IObserver<StatusFrag>
{
    public void MoveAnimation(Vector2 dir);

    public void EndMove();

    public void ChangeNormal();

    public void ChangeStun();

    public void ChangeBurst();

    public void AttackEmote(Vector2 dir);

    public void DodgeEmote(Vector2 dis);

    public void Death();
}
