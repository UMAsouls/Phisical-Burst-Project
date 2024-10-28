

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

    public UniTask AttackEffect(Vector2 pos)
    {
        throw new System.NotImplementedException();
    }

    public async UniTask PawnEffect(Vector2 pawnPos, float size)
    {
        await WaitEffect(pawnPos, MagicCircleEffect, size);
    }

    
}
