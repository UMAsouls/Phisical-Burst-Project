
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using Zenject;

public class EnemyAI : MonoBehaviour
{
    [Inject]
    IPawnGettable strage;

    [Inject]
    ActionMaker actionMaker;

    [Inject]
    CommandBehaviourMakeable behaviourMaker;

    [SerializeField]
    private bool RangeAttackAble;

    [SerializeField]
    private float RangeAttackArea;

    private Dictionary<int, float> HateList;

    private int targetID;
    private float targetHate;
    
    public void EnemySelect(IEnemyPawn enemy)
    {
        AttackAble target = strage.GetPawnByID<AttackAble>(targetID);

        float dis = (target.Position - enemy.VirtualPos).magnitude;

        bool attack = dis <= enemy.range + enemy.AttackRange + target.Size / 2;
        bool rangeAttack = dis <= RangeAttackArea;

        if (RangeAttackAble && rangeAttack) { RangeCmdSelect(target, enemy); }
        else if (attack) { AttackSelect(target, enemy); }
        else { MoveOrAmbush(target.Position, target.Size, enemy); }
    }

    private void RangeCmdSelect(AttackAble target, IEnemyPawn enemy)
    {
        while (enemy.ActPoint > 0)
        {
            float dis = (target.Position - enemy.VirtualPos).magnitude;
            bool attack = dis <=  enemy.AttackRange/2 + target.Size/2;

            IAction act;
            if(Random.Range(0,2) == 0)
            {
                act = MakeRangeAttackAct(enemy);
            }else
            {
                if(attack) act = MakeAttackAction(target, enemy);
                else act = MakeMoveAct(target.Position, target.Size, enemy);
            }

            act.setAct(strage.GetPawnByID<ActionSettable>(enemy.ID));
        }
    }

    private void AttackSelect(AttackAble target, IEnemyPawn enemy)
    {
        while(enemy.ActPoint > 0)
        {
            float dis = (target.Position - enemy.VirtualPos).magnitude;
            bool attack = dis <= enemy.AttackRange/2 + target.Size/2;

            IAction act;
            if (attack) act = MakeAttackAction(target, enemy);
            else act = MakeMoveAct(target.Position, target.Size, enemy);

            act.setAct(strage.GetPawnByID<ActionSettable>(enemy.ID));
        }
    }

    private void MoveOrAmbush(Vector2 targetPos, float targetSize, IEnemyPawn enemy)
    {
        while(enemy.ActPoint > 0)
        {
            IAction act;
            if (Random.Range(0, 2) == 0) act = MakeMoveAct(targetPos, targetSize, enemy);
            else act = MakeAmbushAct(enemy);

            act.setAct(strage.GetPawnByID<ActionSettable>(enemy.ID));
        }
    }

    private IAction MakeAttackAction(AttackAble target, IEnemyPawn enemy)
    {
        IAction act;
        IBattleCommand[] cmds = enemy.EnemyBattleSelect();
        if (enemy.ActPoint >= 2) act = actionMaker.MakeHasteAction(cmds, target);
        else act = actionMaker.MakeNormalAttackAction(cmds, target);
        return act;
    }

    private IAction MakeAmbushAct(IEnemyPawn enemy)
    {
        float range;
        if (enemy.ActPoint >= 2) range = enemy.AttackRange * 3f;
        else range = enemy.AttackRange * 1.5f;

        return actionMaker.MakeAmbushAction(range, enemy.ActPoint);
    }

    private IAction MakeMoveAct(Vector2 targetPos, float targetSize, IEnemyPawn enemy)
    {
        Vector2 vec = targetPos - enemy.VirtualPos;
        Vector2 dir = vec.normalized;

        IAction act;
        Vector2 delta;
        if(vec.magnitude <= enemy.range + enemy.AttackRange/2 + targetSize/2)
        {
            var dis = vec.magnitude - targetSize/2 - enemy.AttackRange/2;
            delta = dir * dis;
        }
        else
        {
            delta = dir*enemy.range;
        }

        act = actionMaker.MakeMoveAction(delta);
        return act;
    }

    private IAction MakeRangeAttackAct(IEnemyPawn enemy)
    {
        IActionCommand cmd = enemy.EnemyActCmdSelect();
        IActionCommandBehaviour behaviour = behaviourMaker.MakeRangeBehaviour(cmd, false, PawnType.Member);

        if (behaviour != null) return actionMaker.MakeCommandAction(behaviour);
        else return null;
    }

    public void HateAdd(int targetID, float Hate)
    {
        HateList[targetID] += Hate;
        if(targetHate < HateList[targetID])
        {
            this.targetID = targetID;
            targetHate = HateList[targetID];
        }
    }


    public void TurnEnd()
    {
        foreach (var enemy in HateList) HateList[enemy.Key] -= 10f;
    }

    private async void Start()
    {
        await UniTask.WaitUntil(() => strage.IsSetComplete, cancellationToken: destroyCancellationToken);

        IDGettable[] pawns = strage.GetPawnList<IDGettable>();

        HateList = new Dictionary<int, float>();
        foreach (var pawn in pawns) HateList[pawn.ID] = 0;

        targetID = pawns[0].ID;
    }


}
