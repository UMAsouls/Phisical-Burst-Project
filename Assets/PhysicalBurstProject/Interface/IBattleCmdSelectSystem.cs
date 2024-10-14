using Cysharp.Threading.Tasks;
using System.Collections;
using UnityEngine;

public interface IBattleCmdSelectSystem
{
    public UniTask<int> BattleCmdSelect(int id);
}