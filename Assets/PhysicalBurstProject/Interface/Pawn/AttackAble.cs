using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

public interface AttackAble: IDGettable, PawnTypeGettable
{
    public int HP { get; }

    public string name { get; }

    public Vector2 Position { get; }

    public float attack {  get; }

    public bool IsMove { get; set; }

    public UniTask EmergencyBattle(AttackAble target);

    public IBattleCommand[] EmergencyCmds { get; }

    public int Priority { get; set; }

    public void PhysicalBurst();
    public bool Burst { get; }

    public bool Avoid { get; set; }

    public float Guard { get; set; }

    public bool DamageAble { get; set; }

    public bool AttackEnd {  get; set; }

    public bool IsStun {  get; set; }

    public bool GetAmbushed { get; set; }

    public bool ActionStop { get; set; }

    public float Size { get; }

    public UniTask<bool> Damage(float damage);
    public UniTask<bool> Heal(float heal);
    public UniTask<bool> Crash();

    public void Stun();

    public void FightStart();
    public void FightEnd();

    public void AttackEmote(Vector2 dir);

    public void DodgeEffect(Vector2 dis);

    public void UseMana(int m);

    public void MiniStatusPrint();
    public void MiniStatusDestroy();
    
}
