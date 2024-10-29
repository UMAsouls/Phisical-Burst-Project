using Cysharp.Threading.Tasks;
using System.Collections;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyPawn : BattlePawn, IEnemyPawn
{
    public override PawnType Type => PawnType.Enemy;

    private EnemyAI ai;

    public override async UniTask EmergencyBattle()
    {
        emergencyCmds = EnemyBattleSelect();
    }

    public T[] EnemyCmdSelect<T>(T[] cmdList, int len) where T: ICommand
    {
        float pSum = GetCmdPrioritySum<T>(cmdList);
        T[] outCmds = new T[len];

        for (int i = 0; i < outCmds.Length; i++)
        {
            T selectCmd = default(T);
            float p = 0;
            float r = Random.Range(0, pSum);
            foreach (var cmd in cmdList)
            {
                if (r <= p + cmd.SelectPriority) { selectCmd = cmd; break; }
                else p += cmd.SelectPriority;
            }
            outCmds[i] = selectCmd;
        }

        return outCmds;
    }

    public float GetCmdPrioritySum<T>(T[] cmdList) where T: ICommand
    {
        float sum = 0;
        foreach (var cmd in cmdList)
        {
            sum += cmd.SelectPriority;
        }
        return sum;
    }

    public IBattleCommand[] EnemyBattleSelect()
    {
        return EnemyCmdSelect<IBattleCommand>(BattleCommands, 3);
    }

    public IActionCommand EnemyActCmdSelect()
    {
        return EnemyCmdSelect(ActionCommands, 1)[0];
    }

    public void EnemySelect()
    {
        ai.EnemySelect(this);
    }


    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        ai = GetComponent<EnemyAI>();
    }

    // Update is called once per frame
    protected override void Update()
    {

    }
}