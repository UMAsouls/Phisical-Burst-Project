
using System;
using UnityEngine;

[Serializable]
public class HealCommand : ActionCommand<IHealCommand>, IHealCommand
{
    [SerializeField]
    private float range;
    public float Range => range;

    [SerializeField]
    private float heal;
    public float Heal => heal;

    public override ActionCmdType Type => ActionCmdType.Heal;
}
