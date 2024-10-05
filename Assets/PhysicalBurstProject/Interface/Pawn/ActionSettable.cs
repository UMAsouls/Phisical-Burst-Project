

using UnityEngine;

public interface ActionSettable : IDGettable
{
    public Vector2 VirtualPos { get; }
    public int ActionNum { get; }
    public void ActionAdd(IAction action);

    public float range { get; }
}
