using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Utilities;

[Serializable]
public class Status : IStatus
{
    protected IAccessorie[] accessories;

    protected List<IObserver<IStatus>> statusObservers;
    protected List<IObserver<StatusFrag>> statusFragObservers;
    protected List<IObserver<DamageEffectFrag>> damageEffectFragObservers;

    [SerializeField]
    private int maxhp;
    [SerializeField]
    private int hp;

    [SerializeField]
    private string name;

    private float attack;
    [SerializeField]
    private float attackBase;

    private float defence;
    [SerializeField]
    private float defenceBase;

    private float speed;
    [SerializeField]
    private float speedBase;

    private float range;
    [SerializeField]
    private float rangeBase;

    private float attackRange;
    [SerializeField]
    private float attackRangeBase;

    private int priority = 0;

    private bool stun = false;
    private bool burst = false;
    private bool death = false;

    private bool avoid = false;
    private float guard = 0;

    private int mana;

    //ƒQƒbƒ^[
    public string Name => name;

    public float Attack => attack;
    public float AttackBase => attackBase;

    public float Defence => defence;
    public float DefenceBase => defenceBase;

    public float Speed => speed;
    public float SpeedBase => speedBase;

    public float Range => range;
    public float RangeBase => rangeBase;

    public int HP => hp;
    public int MaxHP => maxhp;

    public float AttackRange => attackRange;
    public float AttackRangeBase => attackRangeBase;

    public int Priority { get => priority; set => priority = Mathf.Clamp(value, 0, 5); }

    public bool IsStun => stun;
    public bool IsBurst => burst;

    public int Mana => mana;

    public bool IsDeath => death;

    public bool Avoid { get => avoid; set => avoid = value ; }
    public float Guard { get => guard; set => guard = Mathf.Clamp(value, 0, 100); }

    public void SetStun() { stun = true; }

    public void TurnStart()
    {
        stun = false;
        burst = false;
        InitPriority();
    }

    public void InitPriority()
    {
        priority = 3;
    }

    public void init()
    {
        hp = maxhp;
        attack = attackBase;
        speed = speedBase;
        defence = defenceBase;
        range = rangeBase;
        attackRange = attackRangeBase;
        InitPriority();
        statusObservers = new List<IObserver<IStatus>>();
        statusFragObservers = new List<IObserver<StatusFrag>>();
        damageEffectFragObservers = new List<IObserver<DamageEffectFrag>>();
    }

    public void Burst()
    {
        burst = true;
        priority = Mathf.Clamp(priority + 1, 0, 5);
        hp -= maxhp / 5;
        ObserverSupport.BroadCastMessage(statusObservers, this);
        ObserverSupport.BroadCastMessage(statusFragObservers, StatusFrag.Burst);
    }

    public void Stun()
    {
        stun = false;
        ObserverSupport.BroadCastMessage(statusFragObservers, StatusFrag.Stun);
    }

    public int Damage(float damage)
    {
        if(Avoid)
        {
            ObserverSupport.BroadCastMessage(
                damageEffectFragObservers,
                DamageEffectFrag.Avoid
            );
            return 0;
        }

        if(burst) Guard = Mathf.Max(Guard, 50f);

        //ƒK[ƒh‚É‚æ‚éŒ¸­
        damage *= 1f - Guard * 0.01f;
        //–hŒä—Í‚É‚æ‚éŒ¸­
        damage = Mathf.Clamp(damage - defence * 1.5f, 0, 99999);

        if(damage >= MaxHP/3)
        {
            ObserverSupport.BroadCastMessage(
                damageEffectFragObservers,
                DamageEffectFrag.BigDamage
            );
        }else
        {
            ObserverSupport.BroadCastMessage(
                damageEffectFragObservers,
                DamageEffectFrag.Damage
            );
        }

            hp -= (int)damage;
        ObserverSupport.BroadCastMessage(statusObservers, this);
        return (int)damage;
    }

    public int Heal(float heal)
    {
        int bef = hp;
        hp = Mathf.Clamp(hp + (int)heal, 0, maxhp);
        ObserverSupport.BroadCastMessage(statusObservers, this);

        return hp - bef;
    }

    public bool UseMana(int use)
    {
        if (mana < use) return false;
        mana -= use;
        ObserverSupport.BroadCastMessage(statusObservers, this);
        return true;
    }

    public void Subscribe(IObserver<IStatus> observer)
    {
        statusObservers.Add(observer);
    }

    public void Subscribe(IObserver<StatusFrag> observer)
    {
        statusFragObservers.Add(observer);
    }

    public void OnComplete()
    {
        throw new NotImplementedException();
    }

    public void OnNext(TurnPhaseFrag value)
    {
        switch(value)
        {
            case (TurnPhaseFrag.TurnEnd):
                burst = false;
                stun = false;
                break;
        }
    }

    public void Subscribe(IObserver<DamageEffectFrag> observer)
    {
        damageEffectFragObservers.Add(observer);
    }
}
