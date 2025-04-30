using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class TestPawnMaker : MonoBehaviour
{
    [Serializable]
    class AllPawn
    {
        [SerializeField]
        GameObject pawn;

        [SerializeField]
        Vector2 position;

        [SerializeReference, SubclassSelector]
        IStatus status;

        [SerializeReference, SubclassSelector]
        IActionCommand[] actionCommands;

        [SerializeReference, SubclassSelector]
        IBattleCommand[] battleCommands;

        public void Init()
        {
            status.init();
        }

        private void OptionSet(PawnOptionSettable statusSettable, int id)
        {
            statusSettable.Status = status;
            statusSettable.ID = id;
            statusSettable.ActionCommands = actionCommands;
            statusSettable.BattleCommands = battleCommands;
        }

        public GameObject MakePawn(DiContainer container, int id)
        {
            var obj = container.InstantiatePrefab(pawn);
            obj.transform.position = position;
            OptionSet(obj.GetComponent<PawnOptionSettable>(), id);
            return obj;
        }
    }

    [SerializeField]
    AllPawn[] pawns;

    [Inject]
    IPawnStrageable pawnStrageable;

    [Inject]
    DiContainer container;

    int id = 0;

    private void Awake()
    {
        foreach(var p in pawns)
        {
            p.Init();
        }
    }

    private void Start()
    {
        for (int i = 0; i < pawns.Length; i++)
        {
            var pawn = pawns[i];
            var obj = pawn.MakePawn(container, id);
            id++;
            pawnStrageable.AddPawnObj(obj);
        }
        
        pawnStrageable.IsSetComplete = true;
    }
}
