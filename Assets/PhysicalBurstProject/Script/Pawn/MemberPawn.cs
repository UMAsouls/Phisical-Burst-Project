using Cysharp.Threading.Tasks;
using System.Collections;
using System.Threading;
using UnityEngine;
using static UnityEngine.ParticleSystem;
using UnityEngine.InputSystem;

public class MemberPawn : BattlePawn
{
    public override PawnType Type => PawnType.Member;

    public override UniTask EmergencyBattle()
    {
        throw new System.NotImplementedException();
    }

    private bool isBurst;

    public void OnBurst(InputAction.CallbackContext context)
    {
        if (context.performed) isBurst = true;
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