using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPawnInfoUI 
{
    public void SetHP(int MaxHP, int HP);
    public string Name { set; }
}
