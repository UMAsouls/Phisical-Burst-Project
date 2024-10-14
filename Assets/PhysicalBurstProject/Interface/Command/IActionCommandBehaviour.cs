using Cysharp.Threading.Tasks;
using System.Collections;
using UnityEngine;

public interface IActionCommandBehaviour
{

    public UniTask DoAction(int pawnID);
}