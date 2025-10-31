using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//CommandStrageÇêVÇµÇ¢édólÇ…çÏÇËíºÇ∑
public class CommandStrage : MonoBehaviour, ICommandStrage
{
    Dictionary<string, IActionCommand> actionDict = new Dictionary<string, IActionCommand>();
    Dictionary<string, IBattleCommand> battleDict = new Dictionary<string, IBattleCommand>();

    private T GetCmd<T>(string key, Dictionary<string, T> dict) where T: ICommand
    {
        if(dict.TryGetValue(key, out T cmd)){ return cmd;  }
        else
        {
            Debug.LogError($"Command with key '{key}' not found.");
            return default;
        }
    }

    private T GetCmd<T>(CommandPackage key, Dictionary<string, T> dict) where T : ICommand
    {
        if (!dict.TryGetValue(key.cmdName, out T cmd)) 
        {
            Debug.LogError($"Command with key '{key}' not found.");
            return default;
        }

        var command = dict[key.cmdName].Copy();
        command.SelectPriority = key.priority;
        return (T)command;
    }

    private T[] GetCmds<T>(string[] keys, Dictionary<string, T> dict) where T : ICommand
    {
        T[] cmds = new T[keys.Length];
        for(int i = 0; i < keys.Length; i++)
        {
            cmds[i] = GetCmd<T>(keys[i], dict);
        }
        return cmds;
    }

    private T[] GetCmds<T>(CommandPackage[] keys, Dictionary<string, T> dict) where T : ICommand
    {
        T[] cmds = new T[keys.Length];
        for (int i = 0; i < keys.Length; i++)
        {
            cmds[i] = GetCmd<T>(keys[i], dict);
        }
        return cmds;
    }



    public IActionCommand[] GetActCmds(string[] keys)
    {
        return GetCmds<IActionCommand>(keys, actionDict);
    }

    public IActionCommand[] GetActCmds(CommandPackage[] keys)
    {
        return GetCmds<IActionCommand>(keys, actionDict);
    }

    public IActionCommand GetActionCommand(string key)
    {
        return GetCmd<IActionCommand>(key, actionDict);
    }

    public IBattleCommand[] GetBattleCmds(string[] keys)
    {
        return GetCmds<IBattleCommand>(keys, battleDict);
    }

    public IBattleCommand[] GetBattleCmds(CommandPackage[] keys)
    {
        return GetCmds<IBattleCommand>(keys, battleDict);
    }

    public IBattleCommand GetBattleCommand(string key)
    {
        return GetCmd<IBattleCommand>(key, battleDict);
    }

    void Awake()
    {
        Object[] actionCmds = Resources.LoadAll("Commands/ActionCommand", typeof(IActionCommand));
        foreach (IActionCommand cmd in actionCmds)
        {
            actionDict.Add(cmd.Name, cmd);
        }
        Object[] battleCmds = Resources.LoadAll("Commands/BattleCommand", typeof(IBattleCommand));
        foreach (IBattleCommand cmd in battleCmds)
        {
            battleDict.Add(cmd.Name, cmd);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
