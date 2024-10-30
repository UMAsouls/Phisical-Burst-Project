using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class RangeAttackCommand : ActionCommand<IRangeAttackCommand>, IRangeAttackCommand
{
    public override ActionCmdType Type => ActionCmdType.RangeAttack;

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

    [SerializeField]
    protected AudioClip pawnEffectSound;
    public AudioClip PawnEffectSound => pawnEffectSound;

    [SerializeField]
    protected AudioClip attackEffectSound;
    public AudioClip AttackEffectSound => attackEffectSound;

    public async UniTask PawnEffect(Vector2 pawnPos, float size)
    {
        await WaitEffect(pawnPos, MagicCircleEffect, size);
    }

    public async UniTask AttackEffect(Vector2 pos)
    {
        await WaitEffect(pos, ExplodeEffect, ExplodeSize * range);
    }
}