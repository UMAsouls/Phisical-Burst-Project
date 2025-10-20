using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

public class PawnActionManger : 
    IPawnActionManager, PawnComponent, IObserver<TurnPhaseFrag>,
    IInitializable
{
    [Inject]
    IObservable<TurnPhaseFrag> TurnPhazePublisher;

    [Inject]
    IActionUnit actUnit;

    private int actPoint;
    private int actMax = 2;

    private int id;
    private PawnType type;

    private bool actionStop;

    private List<IAction> actions;

    private IActionCommand[] actCmds;
    private IBattleCommand[] battleCmds;
    protected IBattleCommand[] emergencyCmds;

    private float burstCostDamage;

    public IBattleCommand[] BattleCommands => battleCmds;
    public IActionCommand[] ActionCommands => actCmds;

    public float BurstCostDamage => burstCostDamage;

    public int ID => id;

    public PawnType Type => type;

    public int ActPoint => actPoint;

    public void ActionAdd(IAction action)
    {
        actions.Add(action);
    }

    public bool UseActPoint(int point)
    {
        if (actPoint < point) return false;

        actPoint = Mathf.Clamp(actPoint - point, 0, actMax);
        return true;
    }

    public IActionCommand[] GetActionCommands()
    {
        return actCmds;
    }

    public void init(int  id, PawnType type)
    {
        this.id = id; this.type = type;
        actPoint = actMax;
        actions = new List<IAction>();
    }

    public async UniTask DoAction()
    {
        actionStop = false;
        foreach (var action in actions)
        {
            if (!actionStop) await action.DoAct(actUnit);
        }
    }

    public string[] GetActionNames()
    {
        string[] names = new string[actions.Count];
        for (int i = 0; i < names.Length; i++)
        {
            names[i] = actions[i].GetActionName();
        }
        return names;
    }

    public void OnComplete()
    {
        throw new NotImplementedException();
    }

    public void OnNext(TurnPhaseFrag value)
    {
        switch(value)
        {
            case TurnPhaseFrag.TurnStart:
                actPoint = actMax;
                break;
            case TurnPhaseFrag.TurnEnd:
                actions = new List<IAction>();
                break;
        }
    }

    public void Initialize()
    {
        TurnPhazePublisher.Subscribe(this);
    }
}
