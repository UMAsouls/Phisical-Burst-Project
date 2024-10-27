using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

public interface AttackAble: IDGettable, PawnTypeGettable
{
    public int HP { get; }

    public string name { get; }

    public Vector2 Position { get; }

    public float attack {  get; }

    public bool IsMove { get; }

    public UniTask EmergencyBattle();

    public IBattleCommand[] EmergencyCmds { get; }

    public int Priority { get; set; }

    public void PhysicalBurst();
    public bool Burst { get; }

    public bool Avoid { get; set; }

    public float Guard { get; set; }

    public bool DamageAble { get; set; }

    public bool AttackEnd {  get; set; }

    public UniTask<bool> Damage(float damage);

    public void Stun();

    public void FightStart();
    public void FightEnd();
}
