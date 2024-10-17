using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class ActionCommand<V> : IActionCommand
{
    [SerializeField]
    protected string name;
    public string Name => name;

    public abstract ActionCmdType Type { get; }

    [SerializeField]
    protected float mana;
    public float UseMana => mana;

    [SerializeField]
    protected float burstRatio;
    public float BurstRatio => burstRatio;

    public T GetMySelf<T>()
    {
        if (typeof(T) == typeof(V)) return (T)(object)this;
        else return default(T);
    }
}
