
using Cysharp.Threading.Tasks;
using UnityEngine;

public interface IEnemyPawn: IDGettable, PosGetPawn
{
    public Vector2 VirtualPos { get; set; }
    public float range { get; }
    public float AttackRange { get; }
    public int ActPoint { get; }

    public IBattleCommand[] EnemyBattleSelect(AttackAble target);
    public IActionCommand EnemyActCmdSelect();

    public void EnemySelect();
}
