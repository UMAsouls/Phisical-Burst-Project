using Cysharp.Threading.Tasks;
using System.Collections;
using UnityEngine;

public interface IActionCommandBehaviour: ICommand
{

    public UniTask DoAction(int pawnID);

    public bool IsBurst { get; }
}