using Cysharp.Threading.Tasks;
using UnityEngine;


public class DodgeCommand : BattleCommand
{
    public override BattleCommandType Type => BattleCommandType.Dodge;

    [SerializeField]
    [Range(0f, 100f)]
    private float possibility;

    [SerializeField]
    [Range(0, 5)]
    private int Bonus;

    public override UniTask Do(AttackAble pawn, AttackAble target, BattleCommandType targetType)
    {
        throw new System.NotImplementedException();
    }
}
