using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public interface IAction 
{
    public ActionType Type { get; }

    public  UniTask DoAct(IActionUnit actUnit);

    public bool setAct(IPawnActionManager manager, IVirtualPawn vpawn, IStatus status);

    public bool CancelAct(IPawnActionManager manager, IVirtualPawn vpawn, IStatus status);

    public string GetActionName();
}
