

using UnityEngine;

public interface ActionSettable : IDGettable
{
    public Vector2 VirtualPos { get; set; }
    public int ActionNum { get; }
    public float range { get; }

    public void ActionAdd(IAction action);
    
    public bool useActPoint(int point);
}
