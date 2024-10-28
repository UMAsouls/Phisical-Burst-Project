using Cysharp.Threading.Tasks;
using UnityEngine;

public interface IEasyEffectCommand: IActionCommand
{
    public UniTask PawnEffect(Vector2 pawnPos, float size);
    public UniTask AttackEffect(Vector2 pos);
}