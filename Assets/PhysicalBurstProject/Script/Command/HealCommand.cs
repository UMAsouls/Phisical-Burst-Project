
using Cysharp.Threading.Tasks;
using System;
using System.Drawing;
using UnityEngine;

[CreateAssetMenu(fileName = "HealCommand", menuName = "PBP/Command/Action/HealCommand")]
public class HealCommand : ActionCommand<IHealCommand>, IHealCommand
{
    [SerializeField]
    private float range;
    public float Range => range;

    [SerializeField]
    private float heal;
    public float Heal => heal;

    [SerializeField]
    protected GameObject MagicCircleEffect;

    [SerializeField]
    protected GameObject HealEffect;

    [SerializeField]
    protected float HealEffectSize;

    public override float EffectScale => range;

    public override ActionCmdType Type => ActionCmdType.Heal;

    [SerializeField]
    protected AudioClip pawnEffectSound;
    public AudioClip PawnEffectSound => pawnEffectSound;

    [SerializeField]
    protected AudioClip healEffectSound;
    public AudioClip AttackEffectSound => healEffectSound;

    public HealCommand()
    {
        range = 0f;
        heal = 0f;
        MagicCircleEffect = null;
        HealEffect = null;
        HealEffectSize = 0f;
        pawnEffectSound = null;
        healEffectSound = null;
    }

    public HealCommand(
        ActionCommand<IHealCommand> cmd, float range, float heal, 
        GameObject magicCircleEffect, GameObject healEffect, float healEffectSize, 
        AudioClip pawnEffectSound, AudioClip healEffectSound
        ) : base(cmd)
    {
        this.range = range;
        this.heal = heal;
        MagicCircleEffect = magicCircleEffect;
        HealEffect = healEffect;
        HealEffectSize = healEffectSize;
        this.pawnEffectSound = pawnEffectSound;
        this.healEffectSound = healEffectSound;
    }

    public HealCommand(HealCommand cmd) : 
        this(
        cmd, cmd.range, cmd.heal,
        cmd.MagicCircleEffect, cmd.HealEffect, cmd.HealEffectSize,
        cmd.pawnEffectSound, cmd.healEffectSound
        ) { }

    public async UniTask PawnEffect(Vector2 pawnPos, float size)
    {
        await WaitEffect(pawnPos, MagicCircleEffect, size);
    }

    public async UniTask AttackEffect(Vector2 pos)
    {
        await WaitEffect(pos, HealEffect, HealEffectSize*range);
    }

    public override string GetTypeText()
    {
       return "回復";
    }

    public override IActionCommand Copy() => new HealCommand(this);
}
