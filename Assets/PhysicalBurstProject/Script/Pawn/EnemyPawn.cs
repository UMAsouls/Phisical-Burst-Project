﻿using System.Collections;
using UnityEngine;

public class EnemyPawn : BattlePawn
{
    public override PawnType Type => PawnType.Enemy;

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