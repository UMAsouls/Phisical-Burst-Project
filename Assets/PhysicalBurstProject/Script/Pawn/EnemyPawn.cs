using Cysharp.Threading.Tasks;
using System.Collections;
using System.Threading;
using UnityEngine;

public class EnemyPawn : BattlePawn
{
    public override PawnType Type => PawnType.Enemy;

    public override async UniTask EmergencyBattle()
    {
        float pSum = GetBattleCmdPrioritySum();

        emergencyCmds = new IBattleCommand[3];

        for(int i = 0; i < emergencyCmds.Length; i++)
        {
            IBattleCommand selectCmd = default(IBattleCommand);
            float p = 0;
            float r = Random.Range(0, pSum);
            foreach(var cmd in BattleCommands)
            {
                if(r <= p+cmd.SelectPriority) { selectCmd = cmd; break; }
                else p += cmd.SelectPriority;
            }
            emergencyCmds[i] = selectCmd;
        }
    }

    private float GetBattleCmdPrioritySum()
    {
        float sum = 0;
        foreach(var cmd in BattleCommands)
        {
            sum += cmd.SelectPriority;
        }
        return sum;
    }


    // Use this for initialization
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {

    }
}