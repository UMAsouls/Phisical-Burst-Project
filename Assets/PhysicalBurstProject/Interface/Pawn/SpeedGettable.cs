﻿
using Cysharp.Threading.Tasks;
using UnityEngine;

public interface SpeedGettable
{
    public int ID { get; }
    public float speed { get; }

    public int ActPoint { get; }

    public UniTask TurnStart();
}
