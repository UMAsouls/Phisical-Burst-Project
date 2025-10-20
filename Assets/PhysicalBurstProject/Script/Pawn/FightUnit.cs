using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.YamlDotNet.Core.Tokens;
using UnityEngine;
using Zenject.SpaceFighter;

public class FightUnit : MonoBehaviour,  IFightUnit
{
    IStatus status;
    IPawnBattleInfo info;
    IMiniStatusController controller;

    EffectUnit effectUnit;
    IPawnAnimator animator;

    bool damageAble;
    bool attackEnd;

    List<IObserver<DamageEffectFrag>> damageEffectObservers;

    public int HP => status.HP;

    public Vector2 Position => transform.position;

    public float attack => status.Attack;

    public bool Death => status.IsDeath;

    public bool IsMove { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public IBattleCommand[] EmergencyCmds => throw new System.NotImplementedException();

    public int Priority { get => status.Priority; set => status.Priority = value; }

    public bool Burst => status.IsStun;

    public bool Avoid { get => status.Avoid; set => status.Avoid = value; }
    public float Guard { get => status.Guard; set => status.Guard = value; }
    public bool DamageAble { get => damageAble; set => damageAble = value; }
    public bool AttackEnd { get => attackEnd; set => attackEnd = value; }
    public bool IsStun { get => status.IsStun; }
    public bool GetAmbushed { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public bool ActionStop { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public float Size => throw new System.NotImplementedException();

    public int ID => info.ID;

    public PawnType Type => info.Type;

    public void AttackEmote(Vector2 dir) => animator.AttackEmote(dir);

    public UniTask<bool> Crash()
    {
        throw new System.NotImplementedException();
    }

    public async UniTask<bool> Damage(float damage, Vector2 from_pos)
    {
        //AttackAble from = strage.GetPawnComponentByID<AttackAble>(fromID);
        await UniTask.WaitUntil(() => DamageAble, cancellationToken: destroyCancellationToken);

        var pos = (Vector2)transform.position;
        //var dis = from.Position - Position;
        if (Avoid) { animator.DodgeEmote(pos - from_pos); return false; }

        int d = status.Damage(damage * (1f - Guard * 0.01f));
        effectUnit.Damage(d);

        return true;
    }

    public UniTask EmergencyBattle(AttackAble target)
    {
        throw new System.NotImplementedException();
    }

    public void FightEnd()
    {
        DamageAble = true;
        Avoid = false;
        Guard = 0;
        AttackEnd = true;
    }

    public void FightStart()
    {
        Avoid = false;
        Guard = 0;
        DamageAble = false;
        AttackEnd = false;
    }

    public UniTask<bool> Heal(float heal)
    {
        throw new System.NotImplementedException();
    }

    public void MiniStatusDestroy() => controller.MiniStatusDestroy();

    public void MiniStatusPrint() => controller.MiniStatusPrint();

    public void PhysicalBurst() => status.Burst();

    public void Stun() => status.Stun();

    public void UseMana(int m) => status.UseMana(m);

    // Use this for initialization
    void Start()
    {
        effectUnit = GetComponent<EffectUnit>();
        animator = GetComponent<IPawnAnimator>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}