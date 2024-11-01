
using Cysharp.Threading.Tasks;
using UnityEngine;

public interface ActionSelectable: PawnTypeGettable, IDGettable
{
    public float speed { get; }

    public int ActPoint { get; }

    public bool IsStun {  get; }

    public bool Death {  get; }

    public string name { get; }

    public UniTask TurnStart();

    public UniTask TurnEnd();

    public void SelectStart();

    public void SelectEnd();

    public bool CancelSelect();

    public string[] GetActionNames();

    public UniTask DoAction();
}
