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
        [SerializeReference, SubclassSelector]
        public T value;
    }

    [SerializeField]
    List<MyPair<IActionCommand>> actionCmds;

    Dictionary<string, IActionCommand> actionDict;

    [SerializeField]
    List<MyPair<IBattleCommand>> battleCmds;

    Dictionary<string, IBattleCommand> battleDict;

    public IActionCommand GetActionCommand(string key) => actionDict[key];

    public IBattleCommand GetBattleCommand(string key) => battleDict[key];

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