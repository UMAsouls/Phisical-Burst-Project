using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using UnityEngine;

[Serializable]
public abstract class BattleCommand : IBattleCommand
{
    [SerializeField]
    protected string name;
    public string Name => name;

    [SerializeField]
    protected float mana;
    public float UseMana => mana;

    [SerializeField]
    [Range(0, 100f)]
    private float selectPriority;
    public float SelectPriority { get => selectPriority; set => selectPriority = value; }

    [SerializeField, Multiline(3)]
    private string description;
    public string Description => description;

    public BattleCommand()
    {
        name = "";
        mana = 0;
        selectPriority = 0;
        description = "";
    }

    public BattleCommand(string name, float mana, float selectPriority, string description)
    {
        this.name = name;
        this.mana = mana;
        this.selectPriority = selectPriority;
        this.description = description;
    }

    public BattleCommand(BattleCommand cmd): this(cmd.name, cmd.mana, cmd.selectPriority, cmd.description) { }

    public abstract string GetTypeText();

    public abstract BattleCommandType Type { get; }

    public abstract IBattleCommand Copy();

    public abstract UniTask Do(AttackAble pawn, AttackAble target, BattleCommandType targetType);
}