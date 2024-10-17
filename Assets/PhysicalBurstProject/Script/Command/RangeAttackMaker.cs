
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

public class RangeAttackMaker : CommandMakerBase<IRangeAttackCommand>
{

    [SerializeField]
    private GameObject RangeViewer;

    public override async UniTask<IActionCommandBehaviour> MakeBehaviour(IRangeAttackCommand cmd, int pawnID)
    {
        var vpawn = strage.GetPawnById<IVirtualPawn>(pawnID);
        var obj = Instantiate(RangeViewer, (Vector3)(vpawn.VirtualPos), Quaternion.identity);

        await UniTask.WaitUntil(() => isConfirm | isCancel);

        Destroy(obj);
        if (isConfirm) { return new RangeAttackBehaviour(cmd);  }
        else { return null; }
    }
}
