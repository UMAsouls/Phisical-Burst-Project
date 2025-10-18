using Cysharp.Threading.Tasks;
using UnityEngine;

public class SpellCmdBehaviour : EasyEffectBehaviour<ISpellCommand>
{
    public SpellCmdBehaviour(ISpellCommand cmd, bool burst, PawnType target)
    {
        this.cmd = cmd;
        this.burst = burst;
        this.target = target;
    }

    public override async UniTask DoAction(int pawnID)
    {
        PawnActInterface pawn = strage.GetPawnComponentByID<PawnActInterface>(pawnID);

        await PawnEffect(pawn);

        strage.HateBroadCast(cmd.GetMana/2, pawnID);

        pawn.Spell(cmd.GetMana);
    }

    public override void SetCommand(int pawnID)
    {
        var pawn = strage.GetPawnComponentByID<IBattlePawn>(pawnID);
        var vpawn = pawn.VirtualPawn;

        vpawn.VirtualMana += cmd.GetMana;
    }
}
