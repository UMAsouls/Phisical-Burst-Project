using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAction 
{
    public ActionType Type { get; }

    public  UniTask DoAct(ActablePawn pawn);
}
