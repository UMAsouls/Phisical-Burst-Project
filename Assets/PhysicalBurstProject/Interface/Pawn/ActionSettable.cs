

using UnityEngine;

public interface ActionSettable
{
    public Vector2 VirtualPos { get; }
    public int ActionNum { get; }
    public void ActionAdd(IAction action);
}
