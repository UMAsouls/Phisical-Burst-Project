using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPorn
{
    public float attack {  get; }
    public float defence { get; }
    public float speed { get; }
    public float range { get; }

    public bool death { get; }
}
