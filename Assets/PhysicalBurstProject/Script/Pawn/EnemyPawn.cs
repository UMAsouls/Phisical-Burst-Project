using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyPawn : BattlePawn, IEnemyPawn
{
    public override PawnType Type => PawnType.Enemy;

    private EnemyAI ai;

    public override async UniTask EmergencyBattle(AttackAble target)
    {
        emergencyCmds = EnemyBattleSelect(target);
        await UniTask.Delay(100);
    }

    public override async UniTask<IBattleCommand[]> AmbushSelect(AttackAble target) => EnemyBattleSelect(target);

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

    public IBattleCommand[] EnemyBattleSelect(AttackAble target)
    {
        float pSum = 0;
        Dictionary<IBattleCommand, float> pDict = new Dictionary<IBattleCommand, float>();

        foreach (var cmd in BattleCommands)
        {
            float p = cmd.SelectPriority;
            Debug.Log($"入れる前: {cmd.Name}: 優先度{cmd.SelectPriority}");
            switch(cmd.Type)
            {
                case (BattleCommandType.Strong):
                    p += 30f * (Priority - target.Priority);
                    break;
                case (BattleCommandType.Weak):
                    p += 20f * (Priority - target.Priority);
                    break;
                case (BattleCommandType.Dodge):
                    p += 20f * (target.Priority - Priority);
                    break;
                case (BattleCommandType.Defence):
                    p +=  10f * (target.Priority - Priority);
                    break;
            }
            pDict[cmd] = p;
            pSum += p;
        }

        IBattleCommand[] outCmds = new IBattleCommand[3];

        for (int i = 0; i < outCmds.Length; i++)
        {
            IBattleCommand selectCmd = null;
            float p = 0;
            float r = Random.Range(0, pSum);
            foreach (var pair in pDict)
            {
                Debug.Log($"{pair.Key.Name}: 優先度{pair.Value}");
                if (r <= p + pair.Value) { selectCmd = pair.Key; break; }
                else p += pair.Value;
            }
            if (selectCmd == null) selectCmd = BattleCommands[0];
            outCmds[i] = selectCmd;
        }

        return outCmds;
    }

    public IActionCommand EnemyActCmdSelect()
    {
        return EnemyCmdSelect(ActionCommands, 1)[0];
    }

    public void EnemySelect()
    {
        ai.EnemySelect(this);
    }

    public override async UniTask<bool> Damage(float damage, int fromID)
    {
        bool ans = await base.Damage(damage, fromID);
        ai.HateAdd(fromID, damage*0.6f);
        return ans;
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