using UnityEngine;

public interface BefActPawn
{
    Vector2 VirtualPos { set; }

    public bool useActPoint(int point);
}
