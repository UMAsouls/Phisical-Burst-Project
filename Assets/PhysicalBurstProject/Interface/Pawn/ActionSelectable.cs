
using Cysharp.Threading.Tasks;
using UnityEngine;

public interface ActionSelectable
{
    public int ID { get; }
    public float speed { get; }

    public int ActPoint { get; }

    public UniTask TurnStart();

    public UniTask TurnEnd();

    public bool CancelSelect();

    public string[] GetActionNames();
}
