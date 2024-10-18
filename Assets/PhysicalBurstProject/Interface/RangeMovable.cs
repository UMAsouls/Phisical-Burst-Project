using System.Collections;
using UnityEngine;

public interface RangeMovable 
{
    public void SetMoveDir(Vector3 dir);

    public float Range { set; }

    public void SetFirstPos();
}