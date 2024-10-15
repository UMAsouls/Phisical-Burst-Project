
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

public class RangeAttackMaker : CommandMakerBase<IRangeAttackCommand>
{

    public override async UniTask<IActionCommandBehaviour> MakeBehaviour(IRangeAttackCommand cmd)
    {
        await UniTask.WaitUntil(() => isConfirm | isCancel);
        if(isConfirm) { return new RangeAttackBehaviour(cmd);  }
        else { return null; }
    }
}
