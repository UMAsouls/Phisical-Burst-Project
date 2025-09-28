using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CommandStrage : MonoBehaviour, ICommandStrage
{
    [Serializable]
    public class MyPair<T> where T:  ICommand
    {
        [SerializeField]
        public string key;
        [SerializeField]
        public T value;
    }

    [SerializeField]
    List<MyPair<IActionCommand>> actionCmds;

    Dictionary<string, IActionCommand> actionDict;

    [SerializeField]
    List<MyPair<IBattleCommand>> battleCmds;

    Dictionary<string, IBattleCommand> battleDict;

    public IActionCommand GetActionCommand(string key) => actionDict[key].Copy();
    public IActionCommand GetActionCommand(CommandPackage key)
    {
        var cmd = actionDict[key.cmdName];
        cmd.SelectPriority = key.priority;
        return cmd;
    }

    public IActionCommand[] GetActCmds(string[] keys)
    {
        IActionCommand[] actCmds = new IActionCommand[keys.Length];
        for(int i = 0; i < keys.Length; i++) actCmds[i] = GetActionCommand(keys[i]);
        return actCmds;
    }
    public IActionCommand[] GetActCmds(CommandPackage[] keys)
    {
        IActionCommand[] actCmds = new IActionCommand[keys.Length];
        for (int i = 0; i < keys.Length; i++) actCmds[i] = GetActionCommand(keys[i]);
        return actCmds;
    }

    public IBattleCommand GetBattleCommand(string key) => battleDict[key].Copy();
    public IBattleCommand GetBattleCommand(CommandPackage key)
    {
        var cmd = battleDict[key.cmdName].Copy();
        cmd.SelectPriority = key.priority;
        Debug.Log($"{key.cmdName} : {key.priority}");
        return cmd;
    }

    public IBattleCommand[] GetBattleCmds(string[] keys)
    {
        IBattleCommand[] battleCmds = new IBattleCommand[keys.Length];
        for (int i = 0;i < keys.Length; i++) battleCmds[i] = GetBattleCommand(keys[i]);
        return battleCmds;
    }

    public IBattleCommand[] GetBattleCmds(CommandPackage[] keys)
    {
        IBattleCommand[] battleCmds = new IBattleCommand[keys.Length];
        for (int i = 0; i < keys.Length; i++) battleCmds[i] = GetBattleCommand(keys[i]);
        return battleCmds;
    }

    private void Awake()
    {
        actionDict = new Dictionary<string, IActionCommand>();
        foreach(var act in actionCmds) actionDict[act.key] = act.value;

        battleDict = new Dictionary<string, IBattleCommand>();
        foreach(var battle in battleCmds) battleDict[battle.key] = battle.value;
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}