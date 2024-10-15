using Cysharp.Threading.Tasks;
using System.Collections;
using UnityEngine;

public abstract class CommandMakerBase<T> : ConfirmCancelCatchAble
{
    public abstract UniTask<IActionCommandBehaviour> MakeBehaviour(T cmd);
}