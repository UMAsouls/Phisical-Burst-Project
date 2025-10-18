

using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

public class LongRangeBehaviour : EasyEffectBehaviour<ILongRangeAttackCommand>
{

    private Vector2 pos;

    public LongRangeBehaviour(ILongRangeAttackCommand cmd, bool burst, Vector2 pos, PawnType target)
    {
        this.cmd = cmd;
        this.burst = burst;
        this.pos = pos;
        this.target = target;
    }

    public override async UniTask DoAction(int pawnID)
    {
        PawnActInterface pawn = strage.GetPawnComponentByID<PawnActInterface>(pawnID);

        await PawnEffect(pawn);

        await MainEffect(pos);

        List<AttackAble> pawns = strage.GetPawnsInArea<AttackAble>(pos, cmd.Range/2);
        foreach(AttackAble p in pawns)
        {
            if (p.Type == target) await p.Damage(cmd.Damage*pawn.attack/10, pawnID);
        }

        strage.HateBroadCast(cmd.Damage * pawn.attack / 10 / 10, pawnID);

        await UniTask.Delay(500);
        camerChanger.ChangeToPawnCamera(pawnID);
    }

    public override void SetCommand(int pawnID)
    {
    }
}
