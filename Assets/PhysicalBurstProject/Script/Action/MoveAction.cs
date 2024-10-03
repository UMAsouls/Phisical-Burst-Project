using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

public class MoveAction : IAction
{
    public MoveAction(Vector2 delta)
    {
        this.delta = delta;
    }

    public ActionType Type => ActionType.Move;

    private Vector2 delta;

    public async UniTask DoAct(ActablePawn pawn)
    {
        await pawn.movePos(delta);
    }
}
