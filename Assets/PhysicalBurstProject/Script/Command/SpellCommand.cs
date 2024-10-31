

using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

[Serializable]
public class SpellCommand : ActionCommand<ISpellCommand>, ISpellCommand
{
    public override ActionCmdType Type => ActionCmdType.Spell;

    public override float EffectScale => 0;

    [SerializeField]
    private int getMana;

    [SerializeField]
    private GameObject MagicCircleEffect;

    public int GetMana => getMana;

    [SerializeField]
    protected AudioClip pawnEffectSound;
    public AudioClip PawnEffectSound => pawnEffectSound;

    [SerializeField]
    protected AudioClip attackEffectSound;
    public AudioClip AttackEffectSound => attackEffectSound;

    public SpellCommand(ActionCommand<ISpellCommand> cmd, int getMana, GameObject magicCircleEffect, AudioClip pawnEffectSound, AudioClip attackEffectSound)
        : base(cmd)
    {
        this.getMana = getMana;
        MagicCircleEffect = magicCircleEffect;
        this.pawnEffectSound = pawnEffectSound;
        this.attackEffectSound = attackEffectSound;
    }

    public SpellCommand(SpellCommand cmd)
        : this(cmd, cmd.getMana, cmd.MagicCircleEffect, cmd.pawnEffectSound, cmd.attackEffectSound) { }

    public UniTask AttackEffect(Vector2 pos)
    {
        throw new System.NotImplementedException();
    }

    public async UniTask PawnEffect(Vector2 pawnPos, float size)
    {
        await WaitEffect(pawnPos, MagicCircleEffect, size);
    }

    public override string GetTypeText()
    {
        return "詠唱";
    }

    public override IActionCommand Copy() => new SpellCommand(this);
}
