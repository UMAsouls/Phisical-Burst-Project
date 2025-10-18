using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class PawnActionManger : IPawnActionManager, PawnComponent
{
    private int actPoint;
    private int actMax = 2;

    private int id;
    private PawnType type;

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
    }
}
