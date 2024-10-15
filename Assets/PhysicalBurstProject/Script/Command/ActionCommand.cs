using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActionCommand<V> : IActionCommand
{
    protected string name;
    public string Name => name;

    public abstract ActionCmdType Type { get; }

    protected float mana;
    public float UseMana => mana;

    public T GetMySelf<T>()
    {
        if (typeof(T) is V) return (T)(object)this;
        else return default(T);
    }
}
