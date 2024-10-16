using System.Collections;
using UnityEngine;

public interface IVirtualPawn
{
    public Vector2 VirtualPos { get; set; }
    public float VirtualRange { get; set; }
    public float VirtualMana { get; set; }
    public float VirtualHP { get; set; }
    public bool IsBurst { get; set; }
}