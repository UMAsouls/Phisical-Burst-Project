using UnityEngine;

public class DefenceCommand : BattleCommand
{
    public override BattleCommandType Type => BattleCommandType.Defence;

    [SerializeField]
    [Range(0f, 100f)]
    private float ratio;

    [SerializeField]
    [Range(0f, 100f)]
    private float burstRatio;

    [SerializeField]
    private bool Breakable;
}
