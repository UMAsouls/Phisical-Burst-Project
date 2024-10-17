using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class TestPawnMaker : MonoBehaviour
{
    [SerializeField]
    GameObject pawn;

    [Inject]
    IPawnStrageable pawnStrageable;

    [SerializeReference, SubclassSelector]
    IStatus status;

    [SerializeReference, SubclassSelector]
    IActionCommand[] commands;

    int id = 0;

    private void Awake()
    {
        status.init();
    }

    private void Start()
    {
        var obj = Instantiate(pawn, new Vector2(1,1), Quaternion.identity);

        PawnOptionSettable statusSettable = obj.GetComponent<PawnOptionSettable>();
        statusSettable.Status = status;
        statusSettable.ID = id;
        statusSettable.ActionCommands = commands;

        pawnStrageable.AddPawnObj(obj);
        pawnStrageable.IsSetComplete = true;
    }
}
