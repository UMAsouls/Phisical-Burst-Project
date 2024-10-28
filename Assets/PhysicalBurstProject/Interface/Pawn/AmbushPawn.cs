using System.Collections;
using UnityEngine;

public interface AmbushPawn
{
    public float AttackRange { get; }
    public int ActPoint { get; }
    public Vector2 VirtualPos {  get; }
}