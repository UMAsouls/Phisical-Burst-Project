using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPawn
{
    public string Name { get; }

    public int MaxHP { get; }
    public int HP { get; }

    public float attack {  get; }
    public float defence { get; }
    public float speed { get; }
    public float range { get; }

    public bool death { get; }

    public Vector2 Position {  get; }

    public int Mana { get; }
}
