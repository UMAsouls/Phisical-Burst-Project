using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBattleCommand: ICommand
{
    public BattleCommandType Type { get; }

    public UniTask Do(AttackAble pawn, AttackAble target, BattleCommandType targetType);
}
