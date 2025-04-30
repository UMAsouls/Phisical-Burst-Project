using System.Collections;
using UnityEngine;

public interface ICmdSelectorController
{
    public void Move(int dir);
    public void Set(int pos);
}