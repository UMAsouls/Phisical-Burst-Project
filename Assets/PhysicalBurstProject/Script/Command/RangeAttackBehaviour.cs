
using Cysharp.Threading.Tasks;
using System.Collections.Generic;

using UnityEngine;

public class RangeAttackBehaviour : EasyEffectBehaviour<IRangeAttackCommand>
{

    public RangeAttackBehaviour(IRangeAttackCommand cmd, bool burst, PawnType target)
    {
        this.cmd = cmd;
        this.burst = burst;
        this.target = target;
    }

    public override async UniTask DoAction(int pawnID)
    {
        PawnActInterface pawn = strage.GetPawnByID<PawnActInterface>(pawnID);

        await PawnEffect(pawn);

        await MainEffect(pawn.Position);

        List<AttackAble> pawns = strage.GetPawnsInArea<AttackAble>(pawn.Position, cmd.Range);
        foreach (AttackAble p in pawns)
        {
            if (p.Type == target) await p.Damage(cmd.Damage * pawn.attack / 10, pawnID);
        }

        await UniTask.Delay(500);
        camerChanger.ChangeToPawnCamera(pawnID);
    }
}
