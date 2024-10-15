using Cysharp.Threading.Tasks;
using System.Collections;
using UnityEngine;

public class LongRangeMaker : CommandMakerBase<ILongRangeAttackCommand>
{
    private Vector2 pos;

    public override async UniTask<IActionCommandBehaviour> MakeBehaviour(ILongRangeAttackCommand cmd)
    {


        await UniTask.WaitUntil(() => isConfirm | isCancel);
        if (isConfirm) { return new LongRangeBehaviour(cmd, pos); }
        else { return null; }
    }
}