using Cysharp.Threading.Tasks;
using UnityEngine;

public interface ActablePawn
{
    public UniTask MovePos(Vector2 delta);

    public UniTask Battle(IBattleCommand[] cmds, AttackAble pawn);

    public UniTask Action(IActionCommandBehaviour action);
}
