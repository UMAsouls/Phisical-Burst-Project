using Codice.Client.BaseCommands;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

[Serializable]
public struct CommandPackage
{
    [SerializeField]
    public string cmdName;
    [SerializeField]
    [Range(1f, 50f)]
    public float priority;
}

[CreateAssetMenu(fileName = "PawnPackage", menuName = "PBP/Pawn/PawnPackage")]
public class PawnPackage : ScriptableObject
{
    static CommandPackage[] defaultBattleCmds = new CommandPackage[4];

    [SerializeReference, SubclassSelector]
    IStatus status;

    CommandPackage sattack = new CommandPackage();
    [SerializeField]
    [Range(1f, 50f)]
    float StrongAttackPriority;

    CommandPackage wattack = new CommandPackage();
    [SerializeField]
    [Range(1f, 50f)]
    float WeekAttackPriority;

    CommandPackage defence = new CommandPackage();
    [SerializeField]
    [Range(1f, 50f)]
    float DefencePriority;

    CommandPackage dodge = new CommandPackage();
    [SerializeField]
    [Range(1f, 50f)]
    float DodgePriority;

    [SerializeField]
    CommandPackage[] battleCommands;

    [SerializeField]
    CommandPackage[] actionCommands;

    [SerializeField]
    CommandPackage[] addActCmds;

    [SerializeField]
    CommandPackage[] addBattleCmds;

    [SerializeField]
    GameObject pawn;

    Vector2 position;
    public Vector2 Position { set => position = value; }

    public CommandPackage[] AddBattleCmds => addBattleCmds;
    public CommandPackage[] AddActionCmds => addActCmds;
    public string Name => status.Name;
    public IStatus Status => status;

    [Inject]
    ICommandStrage cmdStrage;

    [Inject]
    DiContainer container;

    [Inject]
    ICommandAdder commandAdder;

    public void Init()
    {
        sattack.cmdName = "強攻撃"; sattack.priority = StrongAttackPriority; defaultBattleCmds[0] = sattack;
        wattack.cmdName = "弱攻撃"; wattack.priority = WeekAttackPriority; defaultBattleCmds[1] = wattack;
        defence.cmdName = "防御"; defence.priority = DefencePriority; defaultBattleCmds[2] = defence;
        dodge.cmdName = "回避"; dodge.priority = DodgePriority; defaultBattleCmds[3] = dodge;
    }

    private void BattleCmdSet(PawnOptionSettable statusSettable, List<int> addBattleCmdindices)
    {
        CommandPackage[] battleCmds =
            new CommandPackage[4 + battleCommands.Length + addBattleCmdindices.Count];

        CommandPackage[] adds = new CommandPackage[addBattleCmdindices.Count];
        for (int i = 0; i < addBattleCmdindices.Count; i++)
        {
            adds[i] = addBattleCmds[addBattleCmdindices[i]];
        }

        defaultBattleCmds.CopyTo(battleCmds, 0);
        battleCommands.CopyTo(battleCmds, 4);
        adds.CopyTo(battleCmds, 4 + battleCommands.Length);

        statusSettable.BattleCommands = cmdStrage.GetBattleCmds(battleCmds);
    }

    private void ActionCmdSet(PawnOptionSettable statusSettable, List<int> addActionCmdindices)
    {
        CommandPackage[] actionCmds =
            new CommandPackage[actionCommands.Length  + addActionCmdindices.Count];

        CommandPackage[] adds = new CommandPackage[addActionCmdindices.Count];
        for (int i = 0; i < addActionCmdindices.Count; i++)
        {
            adds[i] = addActCmds[addActionCmdindices[i]];
        }

        actionCommands.CopyTo(actionCmds, 0);
        adds.CopyTo(actionCmds, actionCmds.Length);

        statusSettable.ActionCommands = cmdStrage.GetActCmds(actionCmds);
    }

    private void OptionSet(PawnOptionSettable statusSettable, int id, AddCommand addCmds)
    {
        var c_status = status.Clone();
        c_status.init();
        statusSettable.Status = c_status;
        statusSettable.ID = id;

        var addBattleCmdindices = addCmds.AddBattleCmdList;
        BattleCmdSet(statusSettable, addBattleCmdindices);
        var addActionCmdIndices = addCmds.AddActionCmdList;
        ActionCmdSet(statusSettable, addActionCmdIndices);
    }

    public GameObject MakePawn(DiContainer container, int id)
    {
        var obj = container.InstantiatePrefab(pawn);
        obj.transform.position = position;
        AddCommand addCommand = commandAdder.GetCommandList(name);

        OptionSet(obj.GetComponent<PawnOptionSettable>(), id, addCommand);
        return obj;
    }
}