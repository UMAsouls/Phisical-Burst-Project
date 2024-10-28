using Cysharp.Threading.Tasks;
using UnityEngine;

public class SpellBehaviourMaker : CommandMakerBase<ISpellCommand>
{
    public override async UniTask<IActionCommandBehaviour> MakeBehaviour(ISpellCommand cmd, int pawnID)
    {
        SelectedPawn pawn = strage.GetPawnByID<SelectedPawn>(pawnID);
        pawn.SelectedFocus();

        await UniTask.WaitUntil(() => (isCancel || isConfirm), PlayerLoopTiming.Update, destroyCancellationToken);

        pawn.SelectedUnFocus();

        var behaviour = new SpellCmdBehaviour(cmd, isBurst, PawnType.Member);
        container.Inject(behaviour);
        if (isConfirm) { return behaviour; }
        else { return null; }
    }

    protected override void Awake()
    {
        base.Awake();
        actionMap = "Spell";
    }
}
