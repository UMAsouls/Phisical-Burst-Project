using UnityEngine;
using UnityEditor;
using System;
using Cysharp.Threading.Tasks;
using static UnityEditor.PlayerSettings;

[Serializable]
public class LongRangeAttackCommand : ActionCommand<ILongRangeAttackCommand>, ILongRangeAttackCommand
{
    [SerializeField]
    private float attackArea;
    public float AttackArea => attackArea;

    [SerializeField]
    private float range;
    public float Range => range;

    [SerializeField]
    private float damage;
    public float Damage => damage;

    [SerializeField]
    protected GameObject MagicCircleEffect;

    [SerializeField]
    protected GameObject ExplodeEffect;

    [SerializeField]
    protected float ExplodeSize;

    public override float EffectScale => range;

    public override ActionCmdType Type => ActionCmdType.LongRangeAttack;

    public async UniTask PawnEffect(Vector2 pawnPos, float size)
    {
        await WaitEffect(pawnPos, MagicCircleEffect, size);
    }

    public async UniTask AttackEffect(Vector2 pos)
    {
        await WaitEffect(pos, ExplodeEffect, ExplodeSize * attackArea);
    }
}
