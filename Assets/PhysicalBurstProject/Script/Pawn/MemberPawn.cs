using Cysharp.Threading.Tasks;
using System.Collections;
using UnityEngine;

public class MemberPawn : BattlePawn
{
    public override PawnType Type => PawnType.Member;

    public override UniTask EmergencyBattle()
    {
        throw new System.NotImplementedException();
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