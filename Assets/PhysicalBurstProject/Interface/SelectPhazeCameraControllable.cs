using Cinemachine;
using UnityEngine;

public interface SelectPhazeCameraControllable: RangeMovable
{
    public Vector2 Position { get;  set; }

    public void SetFirstPos(Vector2 pos);

    public bool RangeMode { get; set; }
}
