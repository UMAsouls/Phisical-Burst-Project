using UnityEngine;
using UnityEditor;
using System;
using Cysharp.Threading.Tasks;

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

    [SerializeField]
    protected AudioClip pawnEffectSound;
    public AudioClip PawnEffectSound => pawnEffectSound;

    [SerializeField]
    protected AudioClip attackEffectSound;
    public AudioClip AttackEffectSound => attackEffectSound;

    public LongRangeAttackCommand()
    {
        attackArea = 0f;
        range = 0f;
        damage = 0f;
        MagicCircleEffect = null;
        ExplodeEffect = null;
        ExplodeSize = 0f;
        pawnEffectSound = null;
        attackEffectSound = null;
    }

    public LongRangeAttackCommand(
        ActionCommand<ILongRangeAttackCommand> cmd,
        float attackArea, float range, float damage,
        GameObject magicCircleEffect, GameObject explodeEffect, float explodeSize,
        AudioClip pawnEffectSound, AudioClip attackEffectSound
        ): base( cmd )
    {
        this.attackArea = attackArea;
        this.range = range;
        this.damage = damage;
        MagicCircleEffect = magicCircleEffect;
        ExplodeEffect = explodeEffect;
        ExplodeSize = explodeSize;
        this.pawnEffectSound = pawnEffectSound;
        this.attackEffectSound = attackEffectSound;
    }

    public LongRangeAttackCommand(LongRangeAttackCommand cmd) :
        this(
            cmd, cmd.AttackArea, cmd.range, cmd.damage,
            cmd.MagicCircleEffect, cmd.ExplodeEffect, cmd.ExplodeSize,
            cmd.pawnEffectSound, cmd.attackEffectSound
            ) { }

    public async UniTask PawnEffect(Vector2 pawnPos, float size)
    {
        await WaitEffect(pawnPos, MagicCircleEffect, size);
    }

    public async UniTask AttackEffect(Vector2 pos)
    {
        await WaitEffect(pos, ExplodeEffect, ExplodeSize * attackArea);
    }

    public override string GetTypeText()
    {
        return "遠距離攻撃";
    }

    public override IActionCommand Copy() => new LongRangeAttackCommand(this);
}
