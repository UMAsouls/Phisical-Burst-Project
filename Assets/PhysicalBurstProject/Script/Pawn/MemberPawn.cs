using Cysharp.Threading.Tasks;
using System.Collections;
using System.Threading;
using UnityEngine;
using static UnityEngine.ParticleSystem;
using UnityEngine.InputSystem;
using Zenject;

public class MemberPawn : BattlePawn
{
    public override PawnType Type => PawnType.Member;

    [Inject]
    IBattleCmdSelectSystem battleCmdSelectSystem;

    private async UniTask<IBattleCommand[]> EmergencySelect(AttackAble target)
    {
        SelectStart();
        IBattleCommand[] cmds;
        while (true)
        {
            cmds = await battleCmdSelectSystem.Select(ID);
            if(cmds != null)  break;
        }
        
        SelectEnd();
        return cmds;
    }

    public override async UniTask EmergencyBattle(AttackAble target)
    {
        emergencyCmds = await EmergencySelect(target);
    }

    public override async UniTask<IBattleCommand[]> AmbushSelect(AttackAble target) => await EmergencySelect(target);

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