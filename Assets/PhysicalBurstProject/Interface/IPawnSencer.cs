using UnityEngine;

public interface IPawnSencer
{
    public bool Senced { get; }
    public AttackAble SencedTarget { get; }

    public PawnType SenceType { set; }
    
    public float Range { get; set; }

    public void Delete();
}
