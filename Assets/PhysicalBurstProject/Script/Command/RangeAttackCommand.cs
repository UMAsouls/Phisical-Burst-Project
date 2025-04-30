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

    public RangeAttackCommand()
    {
        range = 0f;
        damage = 0f;
        MagicCircleEffect = null;
        ExplodeEffect = null;
        ExplodeSize = 0f;
        pawnEffectSound = null;
        attackEffectSound = null;
    }

    public RangeAttackCommand(
        ActionCommand<IRangeAttackCommand> cmd,
        float range, float damage,
        GameObject magicCircleEffect, GameObject explodeEffect, float explodeSize,
        AudioClip pawnEffectSound, AudioClip attackEffectSound
        ) : base(cmd)
    {
        this.range = range;
        this.damage = damage;
        MagicCircleEffect = magicCircleEffect;
        ExplodeEffect = explodeEffect;
        ExplodeSize = explodeSize;
        this.pawnEffectSound = pawnEffectSound;
        this.attackEffectSound = attackEffectSound;
    }

    public RangeAttackCommand(RangeAttackCommand cmd) :
        this(
            cmd, cmd.range, cmd.damage,
            cmd.MagicCircleEffect, cmd.ExplodeEffect, cmd.ExplodeSize,
            cmd.pawnEffectSound, cmd.attackEffectSound
            ) { }

    public async UniTask PawnEffect(Vector2 pawnPos, float size)
    {
        await WaitEffect(pawnPos, MagicCircleEffect, size);
    }

    public async UniTask AttackEffect(Vector2 pos)
    {
        await WaitEffect(pos, ExplodeEffect, ExplodeSize * range);
    }

    public override string GetTypeText()
    {
        return "範囲攻撃";
    }

    public override IActionCommand Copy() => new RangeAttackCommand(this);
}