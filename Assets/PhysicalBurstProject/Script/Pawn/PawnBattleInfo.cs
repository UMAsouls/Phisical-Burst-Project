using UnityEngine;

public class PawnBattleInfo : MonoBehaviour, IPawnBattleInfo
{
    [SerializeField]
    private string name;
    private int id;
    [SerializeField]
    private PawnType type;

    public string Name => name;

    public int ID => id;

    public PawnType Type => type;

    public Vector2 Position => (Vector2) transform.position;

    public void SetID(int id) { this.id = id; }
}
