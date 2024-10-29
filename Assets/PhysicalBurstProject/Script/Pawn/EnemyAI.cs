
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

    [SerializeField]
    private bool RangeAttackAble;

    [SerializeField]
    private float RangeAttackArea;

    private Dictionary<int, float> HateList;

    private int targetID;
    private float targetHate;
    
    public void EnemySelect(IEnemyPawn enemy)
    {
        
    }

    private void MoveOrAmbush(Vector2 targetPos, float targetSize, IEnemyPawn enemy)
    {

    }

    private IAction MakeAmbushAct(IEnemyPawn enemy)
    {
        return null;
    }

    private IAction MakeMoveAct(Vector2 targetPos, float targetSize, IEnemyPawn enemy)
    {
        Vector2 vec = targetPos - enemy.VirtualPos;
        Vector2 dir = vec.normalized;

        IAction act;
        Vector2 delta;
        if(vec.magnitude <= enemy.Range + enemy.AttackRange + targetSize)
        {
            var dis = vec.magnitude - targetSize - enemy.AttackRange;
            delta = dir * dis;
        }
        else
        {
            delta = dir*enemy.Range;
        }

        act = actionMaker.MakeMoveAction(delta);
        return act;
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
