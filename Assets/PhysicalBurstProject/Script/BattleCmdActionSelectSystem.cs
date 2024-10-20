using Cysharp.Threading.Tasks;
using System.Collections;
using UnityEngine;
using Zenject;

public class BattleCmdActionSelectSystem : MonoBehaviour, IBattleCmdActionSelectSystem
{
    [Inject]
    private IPosSelectorUIPrinter posSelectorUIPrinter;

    [Inject]
    private IBattleCmdSelectSystem battleCmdSelectSystem;

    [Inject]
    private IPawnGettable strage;

    public async UniTask<bool> Select(int pawnID)
    {
        posSelectorUIPrinter.PrintPosSelectorUI();

        BattleCmdSelectable pawn = strage.GetPawnByID<BattleCmdSelectable>(pawnID);
        



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