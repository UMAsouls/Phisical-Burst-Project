using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICommand 
{
    string Name { get; }

    public float SelectPriority { get; }

    public float UseMana { get; }
}
