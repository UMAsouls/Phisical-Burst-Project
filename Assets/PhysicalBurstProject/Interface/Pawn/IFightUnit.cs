using Cysharp.Threading.Tasks;

using UnityEngine

public interface IFightUnit
{
    public void FightStart();
    public void FightEnd();

    public int HP { get; }

    public string name { get; }

    public Vector2 Position { get; }

    public float attack { get; }

    public bool Death { get; }

    public bool IsMove { get; set; }

    public UniTask EmergencyBattle(AttackAble target);

    public IBattleCommand[] EmergencyCmds { get; }

    public int Priority { get; set; }

    public void PhysicalBurst();
    public bool Burst { get; }

    public bool Avoid { get; set; }

    public float Guard { get; set; }

    public bool DamageAble { get; set; }

    public bool AttackEnd { get; set; }

    public bool IsStun { get; }

    public bool GetAmbushed { get; set; }

    public bool ActionStop { get; set; }

    public float Size { get; }

    public UniTask<bool> Damage(float damage, Vector2 from_pos);
    public UniTask<bool> Heal(float heal);
    public UniTask<bool> Crash();

    public void Stun();

    public void AttackEmote(Vector2 dir);

    public void UseMana(int m);

    public void MiniStatusPrint();
    public void MiniStatusDestroy();
}
