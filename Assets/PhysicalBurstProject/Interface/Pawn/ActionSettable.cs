

using UnityEngine;

public interface ActionSettable : IDGettable
{
    public Vector2 VirtualPos { get; set; }
    public int ActPoint { get; }
    public float range { get; }

    public void ActionAdd(IAction action);
    
    public bool UseActPoint(int point);

   
}
