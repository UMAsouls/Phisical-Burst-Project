
using Cysharp.Threading.Tasks;
using UnityEngine;

public interface ActionSelectable: PawnTypeGettable, IDGettable
{
    public float speed { get; }

    public int ActPoint { get; }

    public UniTask TurnStart();

    public UniTask TurnEnd();

    public bool CancelSelect();

    public string[] GetActionNames();

    public UniTask DoAction();
}
