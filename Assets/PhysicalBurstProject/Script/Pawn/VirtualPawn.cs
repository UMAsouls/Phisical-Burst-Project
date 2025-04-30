using System.Collections;
using UnityEngine;

public class VirtualPawn : MonoBehaviour, IVirtualPawn
{
    private bool isBurst;

    private float virtualMana;

    private float virtualHP;

    private float virtualRange;

    public Vector2 VirtualPos { get => transform.position; set => transform.position  = (Vector3)value; }
    public float VirtualRange { get => virtualRange; set => virtualRange = value; }
    public float VirtualMana { get => virtualMana; set => virtualMana = value; }
    public float VirtualHP { get => virtualHP; set => virtualHP = value; }
    public bool IsBurst { get => isBurst; set => isBurst = value; }


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}