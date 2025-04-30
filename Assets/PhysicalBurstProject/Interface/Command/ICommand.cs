using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICommand 
{
    string Name { get; }

    public float SelectPriority { get; set; }

    public float UseMana { get; }

    public string Description { get; }

    public string GetTypeText();
}
