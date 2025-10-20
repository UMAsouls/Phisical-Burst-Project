using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

 public interface IPawnActionManager: PawnTypeGettable, IDGettable
 {
    int ActPoint { get; }

    public void ActionAdd(IAction action);
    public bool UseActPoint(int point);
    
    public IBattleCommand[] BattleCommands { get; }
    public IActionCommand[] ActionCommands { get; }

    public float BurstCostDamage { get; }
    public void init(int id, PawnType type);
    public UniTask DoAction();

    public string[] GetActionNames();
}

