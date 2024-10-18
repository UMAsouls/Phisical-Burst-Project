﻿

using UnityEngine;

public interface ActionSettable : IDGettable, IVirtualPawn
{
    public int ActPoint { get; }
    public float range { get; }

    public int MaxHP { get; }

    public void ActionAdd(IAction action);
    
    public bool UseActPoint(int point);

   
}
