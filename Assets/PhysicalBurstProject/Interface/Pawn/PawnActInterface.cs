using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

public interface PawnActInterface : AttackAble
{
    public new Vector2 Position { get; set; }
    public void MoveAnimation(Vector2 dir);

    public void EndMove();
    public void Spell(int m);

    public UniTask AmbushEffect();

    public void SelectStart();
    public void SelectEnd();

    public UniTask<IBattleCommand[]> AmbushSelect(AttackAble target);

}
