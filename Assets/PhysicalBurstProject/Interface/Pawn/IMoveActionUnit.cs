using Cysharp.Threading.Tasks;
using System.Collections;
using UnityEngine;

public interface IMoveActionUnit
{
    public UniTask Move(Vector2 delta, IEmergencyBattleUnit pawn);
}