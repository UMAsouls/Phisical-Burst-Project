using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BattleSystem : MonoBehaviour, CmdConfirmAble
{
    private string[] defaultActions =
    {
        "ˆÚ“®", "PŒ‚", "‘Ò‚¿•š‚¹", "s“®"
    };

    private ICmdSelectablePawn[] pawns;

    [Inject]
    private IBattleUIPrinter uiPrinter;

    [Inject]
    private IPawnGettable pawnGettable;

    private bool isConfirm;

    private int cmdIndex;

    public void CommandConfirm(int index)
    {
        isConfirm = true;
        cmdIndex = index;
    }

    private async UniTask BattleStart()
    {
        await UniTask.WaitUntil(() => pawnGettable.IsSetComplete);

        pawns = pawnGettable.GetPawnList<ICmdSelectablePawn>();
        Debug.Log("pawn get");

        return;
    }

    private async UniTask Battle()
    {
        Debug.Log("BattleStart");
        await BattleStart();

        foreach (var p in pawns)
        {
            Debug.Log("Select faze");
            uiPrinter.PrintPlayerInformation(p);
            
            await Select(p);
        }

        return;
    }

    private async UniTask Select(ICmdSelectablePawn pawn)
    {
        isConfirm = false;
        uiPrinter.PrintCmdSelecter(defaultActions);

        await UniTask.WaitUntil(() => isConfirm);

        Debug.Log("confirm");
        return;
    }

    // Start is called before the first frame update
    void Start()
    {

        Battle().Forget();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
