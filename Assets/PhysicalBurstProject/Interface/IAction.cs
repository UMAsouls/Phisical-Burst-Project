using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public interface IAction 
{
    public ActionType Type { get; }

    public  UniTask DoAct(ActablePawn pawn);

    public bool setAct(ActionSettable pawn);

    public bool CancelAct(ActionSettable pawn);
}
