using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Status : IStatus
{
    protected IAccessorie[] accessories;

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

    //ゲッター
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

    public void init()
    {
        hp = maxhp;
        attack = attackBase;
        speed = speedBase;
        defence = defenceBase;
        range = rangeBase;
        attackRange = attackRangeBase;
    }
    //ゲッター 最後
}
