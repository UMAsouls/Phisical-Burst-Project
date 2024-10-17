using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CommandBehaviourMaker : MonoBehaviour, CommandBehaviourMakeable
{

    Dictionary<ActionCmdType, CommandMakerBase<IActionCommand>> dict;

    [Inject]
    HealMaker healMaker;
    [Inject]
    RangeAttackMaker rangeAttackMaker;
    [Inject]
    LongRangeMaker longRangeMaker;

    public async UniTask<IActionCommandBehaviour> MakeCommandBehaviour(IActionCommand cmd, int pawnID)
    {
        ActionCmdType type  = cmd.Type;

        switch(type)
        {
            case ActionCmdType.Heal:
                await healMaker.MakeBehaviour(cmd.GetMySelf<IHealCommand>(), pawnID); break;
            case ActionCmdType.RangeAttack:
                await rangeAttackMaker.MakeBehaviour(cmd.GetMySelf<IRangeAttackCommand>(), pawnID); break;
            case ActionCmdType.LongRangeAttack:
                await longRangeMaker.MakeBehaviour(cmd.GetMySelf<ILongRangeAttackCommand>(), pawnID); break;  
        }

        return null;
    }


    // Use this for initialization
    void Start()
    {
        dict = new Dictionary<ActionCmdType, CommandMakerBase<IActionCommand>>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}