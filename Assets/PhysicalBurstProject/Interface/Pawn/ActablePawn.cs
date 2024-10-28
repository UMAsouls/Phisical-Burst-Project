using Cysharp.Threading.Tasks;
using System.Runtime.CompilerServices;
using UnityEngine;

public interface ActablePawn
{
    public UniTask MovePos(Vector2 delta);

    public UniTask Battle(IBattleCommand[] cmds, AttackAble pawn);

    public UniTask Action(IActionCommandBehaviour action);

    public UniTask Ambush(float range);
}
