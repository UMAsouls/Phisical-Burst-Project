using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPawnInfoUI 
{
    public void SetHP(int MaxHP, int HP);
    public void SetMana(int mana);
    public string Name { set; }
}
