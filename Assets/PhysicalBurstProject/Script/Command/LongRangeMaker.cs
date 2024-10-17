using Cysharp.Threading.Tasks;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

public class LongRangeMaker : CommandMakerBase<ILongRangeAttackCommand>
{
    private Vector2 pos;

    [SerializeField]
    private GameObject AreaViewer;

    public override async UniTask<IActionCommandBehaviour> MakeBehaviour(ILongRangeAttackCommand cmd, int pawnID)
    {
        var vpawn = strage.GetPawnById<IVirtualPawn>(pawnID);
        var obj = Instantiate(AreaViewer, (Vector3)(vpawn.VirtualPos), Quaternion.identity);

        await UniTask.WaitUntil(() => isConfirm | isCancel);

        Destroy(obj);
        if (isConfirm) { return new LongRangeBehaviour(cmd, pos); }
        else { return null; }
    }
}