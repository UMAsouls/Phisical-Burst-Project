using Cysharp.Threading.Tasks;
using System.Collections;
using UnityEngine;

public interface IBattleActionSelectSystem
{
    public UniTask<int> BattleActionSelect(int id);
}