using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MovePosSelectSystem :MonoBehaviour, PosConfirmAble, MovePosSelectable
{
    private bool isConfirm;

    private bool isCancel;

    private Vector2 pos;

    [SerializeField]
    private GameObject posSelector;

    [Inject]
    private IPawnGettable strage;

    [Inject]
    IPosSelectorUIPrinter uiPrinter;

    [Inject]
    MoveActionMakeable actMaker;

    public void Cancel()
    {
        isCancel = true;
    }

    public void PosConfirm(Vector2 pos)
    {
        isConfirm = true;
        this.pos = pos;
    }

    public async UniTask<bool> MovePosSelect(int id)
    {
        ActionSettable pawn = strage.GetPawnById<ActionSettable>(id);
        isCancel = false;
        isConfirm = false;

        uiPrinter.PrintPosSelectorUI();
        var obj1 = Instantiate(posSelector, pawn.VirtualPos, Quaternion.identity);

        await UniTask.WaitUntil(() => (isCancel || isConfirm)); 

        if (isCancel) return false;

        Vector2 diff = pos - pawn.VirtualPos;
        pawn.ActionAdd(actMaker.MakeMoveAction(diff));

        Destroy(obj1);
        

        uiPrinter.DestroyPosSelectorUI(); 
        return true;
    }
}
