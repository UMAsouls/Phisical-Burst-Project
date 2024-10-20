using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class TestPawnMaker : MonoBehaviour
{
    [SerializeField]
    GameObject pawn;

    [SerializeField]
    GameObject EnemyPawn;

    [Inject]
    IPawnStrageable pawnStrageable;

    [SerializeReference, SubclassSelector]
    IStatus status;

    [SerializeReference, SubclassSelector]
    IActionCommand[] commands;

    [SerializeReference, SubclassSelector]
    IBattleCommand[] battleCommands;

    int id = 0;

    private void Awake()
    {
        status.init();
    }

    private void OptionSet(PawnOptionSettable statusSettable)
    {
        statusSettable.Status = status;
        statusSettable.ID = id;
        statusSettable.ActionCommands = commands;
        statusSettable.BattleCommands = battleCommands;
    }

    private void Start()
    {
        var obj = Instantiate(pawn, new Vector2(1,1), Quaternion.identity);
        OptionSet(obj.GetComponent<PawnOptionSettable>());

        id++;
        var obj2 = Instantiate(EnemyPawn, new Vector2(-2, -2), Quaternion.identity);
        OptionSet(obj2.GetComponent<PawnOptionSettable>());
        

        pawnStrageable.AddPawnObj(obj);
        pawnStrageable.AddPawnObj(obj2);
        pawnStrageable.IsSetComplete = true;
    }
}
