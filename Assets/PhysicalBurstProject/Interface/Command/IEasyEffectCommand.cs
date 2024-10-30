using Cysharp.Threading.Tasks;
using UnityEngine;

public interface IEasyEffectCommand: IActionCommand
{
    public AudioClip PawnEffectSound { get; }
    public AudioClip AttackEffectSound { get; }

    public UniTask PawnEffect(Vector2 pawnPos, float size);
    public UniTask AttackEffect(Vector2 pos);
}