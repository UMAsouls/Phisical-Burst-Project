using Cysharp.Threading.Tasks;
using UnityEngine;

public interface IActionUnit
{
    public UniTask MovePos(Vector2 delta);

    public UniTask Battle(IBattleCommand[] cmds, AttackAble pawn, int priorityBonus);

    public UniTask Action(IActionCommandBehaviour action);

    public UniTask Ambush(float range);
}
