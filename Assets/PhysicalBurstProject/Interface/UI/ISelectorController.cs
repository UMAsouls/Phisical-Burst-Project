using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISelectorController
{
    public float Scale { set; }
    public void Move(Vector2 pos);
}
