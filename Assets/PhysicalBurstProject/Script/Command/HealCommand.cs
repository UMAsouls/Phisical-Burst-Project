
using Cysharp.Threading.Tasks;
using System;
using System.Drawing;
using UnityEngine;

[Serializable]
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
}
