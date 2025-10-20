using UnityEngine;

public interface SystemSEPlayable: IObserver<StatusFrag>
{
    public void ConfirmSE();
    public void CancelSE();
    public void BlockSE();
    public void SelectorMoveSE();
    public void BigDamageSE();
    public void DamageSE();
    public void DodgeSE();
    public void BurstSE();
    public void StunSE();
    public void BattleAlarmSE();
    public void DeathSE();
}
