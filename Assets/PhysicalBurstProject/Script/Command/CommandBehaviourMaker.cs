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
    [Inject]
    SpellBehaviourMaker spellMaker;
    
    public async UniTask<IActionCommandBehaviour> MakeCommandBehaviour(IActionCommand cmd, int pawnID)
    {
        ActionCmdType type  = cmd.Type;

        switch(type)
        {
            case ActionCmdType.Heal:
                return await healMaker.GetBehaviour(cmd.GetMySelf<IHealCommand>(), pawnID);
            case ActionCmdType.RangeAttack:
                return await rangeAttackMaker.GetBehaviour(cmd.GetMySelf<IRangeAttackCommand>(), pawnID);
            case ActionCmdType.LongRangeAttack:
                return await longRangeMaker.GetBehaviour(cmd.GetMySelf<ILongRangeAttackCommand>(), pawnID);
            case ActionCmdType.Spell:
                return await spellMaker.GetBehaviour(cmd.GetMySelf<ISpellCommand>(), pawnID);
        }

        return null;
    }

    public IActionCommandBehaviour MakeRangeBehaviour(IActionCommand cmd, bool burst, PawnType target)
    {
        if (cmd.Type == ActionCmdType.RangeAttack) return new RangeAttackBehaviour(cmd.GetMySelf<IRangeAttackCommand>(), burst, target);
        else return null;
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