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

    public async UniTask<bool> Select(int pawnID)
    {

        BattleCmdSelectable pawn = strage.GetPawnByID<BattleCmdSelectable>(pawnID);

        int select = await pawnSelector.PawnSelect(pawnID, PawnType.Enemy);

        if(select == -1) return false;

        IBattleCommand[] cmds = await battleCmdSelectSystem.Select(pawnID);

        if(cmds == null) return false;

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