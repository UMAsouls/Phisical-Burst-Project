using Cysharp.Threading.Tasks;
using System.Collections;
using UnityEngine;
using Zenject;

public abstract class CommandMakerBase<T> : ConfirmCancelCatchAble
{
    [Inject]
    protected IPawnGettable strage;

    public abstract UniTask<IActionCommandBehaviour> MakeBehaviour(T cmd, int pawnID);
}