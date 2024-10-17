using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;
using Zenject;

public class MovePosSelectSystem :MonoBehaviour, PosConfirmAble, MovePosSelectable
{
    [Inject]
    DiContainer container;

    private bool isConfirm;

    private bool isCancel;

    private Vector2 pos;

    [SerializeField]
    private GameObject posSelector;

    [SerializeField]
    private GameObject posSelectorRangeCircle;

    [Inject]
    private IPawnGettable strage;

    [Inject]
    IPosSelectorUIPrinter uiPrinter;

    [Inject]
    MoveActionMakeable actMaker;

    private PlayerInput input;

    [Inject]
    CameraChangeAble cameraChanger;

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
        input.SwitchCurrentActionMap("Move");

        ActionSettable pawn = strage.GetPawnById<ActionSettable>(id);
        isCancel = false;
        isConfirm = false;

        uiPrinter.PrintPosSelectorUI();

        Vector3 cameraPos = pawn.VirtualPos;
        cameraPos.z = -1;
        var obj1 = container.InstantiatePrefab(posSelector);
        obj1.transform.position = cameraPos;

        var obj2 = container.InstantiatePrefab(posSelectorRangeCircle) ;
        obj2.transform.position = pawn.VirtualPos;
        obj2.transform.localScale = new Vector3(pawn.range*2, pawn.range*2, 1);

        PosSelectorRangeSetter setter = obj1.GetComponent<PosSelectorRangeSetter>();
        setter.Range = pawn.range;

        cameraChanger.ChangeToSelectPhazeCamera();

        await UniTask.WaitUntil(() => (isCancel || isConfirm)); 

        cameraChanger.ChangeToPawnCamera(id);

        if (isCancel)
        {
            Destroy(obj1);
            Destroy(obj2);
            uiPrinter.DestroyPosSelectorUI();
            return false;
        }

        Vector2 diff = pos - pawn.VirtualPos;
        actMaker.MakeMoveAction(diff).setAct(pawn);

        Destroy(obj1);
        Destroy(obj2);

        uiPrinter.DestroyPosSelectorUI(); 
        return true;
    }

    private void Awake()
    {
        input = GetComponent<PlayerInput>();
    }
}
