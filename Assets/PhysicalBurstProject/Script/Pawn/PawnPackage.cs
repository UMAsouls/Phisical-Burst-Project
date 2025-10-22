using System;
using System.Collections;
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

    [Inject]
    ICommandStrage cmdStrage;

    [Inject]
    DiContainer container;

    public void Init()
    {
        status.init();
        sattack.cmdName = "強攻撃"; sattack.priority = StrongAttackPriority; defaultBattleCmds[0] = sattack;
        wattack.cmdName = "弱攻撃"; wattack.priority = WeekAttackPriority; defaultBattleCmds[1] = wattack;
        defence.cmdName = "防御"; defence.priority = DefencePriority; defaultBattleCmds[2] = defence;
        dodge.cmdName = "回避"; dodge.priority = DodgePriority; defaultBattleCmds[3] = dodge;
    }

    private void OptionSet(PawnOptionSettable statusSettable, int id)
    {
        statusSettable.Status = status;
        statusSettable.ID = id;

        CommandPackage[] battleCmds = new CommandPackage[4 + battleCommands.Length];

        defaultBattleCmds.CopyTo(battleCmds, 0);
        battleCommands.CopyTo(battleCmds, 4);

        statusSettable.ActionCommands = cmdStrage.GetActCmds(actionCommands);
        statusSettable.BattleCommands = cmdStrage.GetBattleCmds(battleCmds);

        foreach(var cmd in statusSettable.BattleCommands)
        {
            Debug.Log($"{status.Name} : {cmd.Name} : 優先度{cmd.SelectPriority} ");
        }
    }

    public GameObject MakePawn(DiContainer container, int id)
    {
        var obj = container.InstantiatePrefab(pawn);
        obj.transform.position = position;
        OptionSet(obj.GetComponent<PawnOptionSettable>(), id);
        return obj;
    }
}