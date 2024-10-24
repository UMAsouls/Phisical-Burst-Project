using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

public interface AttackAble: IDGettable, PawnTypeGettable
{
    public int HP { get; }

    public Vector2 Position { get; }

    public void Attack(int attack);

    public bool IsMove { get; }

    public UniTask EmergencyBattle();

    public IBattleCommand[] EmergencyCmds { get; }

    public int Priority { get; set; }

    public void PhysicalBurst();
    public bool Burst { get; }
}
