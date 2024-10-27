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

    public override async UniTask EmergencyBattle()
    {
        emergencyCmds = await battleCmdSelectSystem.Select(ID);
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