using UnityEngine

public interface IPawnBattleInfo
{
    string Name { get; }
    int ID { get; }
    PawnType Type { get; }
    Vector2 Position { get; }

    public void SetID(int id);
}
