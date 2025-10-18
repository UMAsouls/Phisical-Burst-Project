using Cysharp.Threading.Tasks;
using System.Collections;
using UnityEngine;
using Zenject;

public class BattleCmdActionSelectSystem : MonoBehaviour, IBattleCmdActionSelectSystem
{
    [Inject]
    private IBattleCmdSelectSystem battleCmdSelectSystem;

    [Inject]
    private IPawnSelector pawnSelector;

    [Inject]
    private IPawnGettable strage;

    [Inject]
    private AttackActionMakeable actionMaker;

    public async UniTask<bool> Select(int pawnID)
    {

        var pawn = strage.GetPawnComponentByID<IBattlePawn>(pawnID);
        var actManager = pawn.ActionManager;
        var status = pawn.Status;
        var vpawn = pawn.VirtualPawn;

        int select = await pawnSelector.PawnSelect(pawnID, PawnType.Enemy);

        if(select == -1) return false;

        AttackAble target = strage.GetPawnComponentByID<AttackAble>(select);

        IBattleCommand[] cmds = await battleCmdSelectSystem.Select(pawnID);

        if(cmds == null) return false;

        if(actManager.ActPoint >= 2)
        {
            actionMaker.MakeHasteAction(cmds, target).setAct(actManager, vpawn, status);
        }else
        {
            actionMaker.MakeNormalAttackAction(cmds, target).setAct(actManager, vpawn, status);
        }

        return true;
    }


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}