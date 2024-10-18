using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
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

    OrthoCameraZoomAble cameraZoomController;

    private CancellationToken cts;

    public void Cancel()
    {
        isCancel = true;
    }

    public void PosConfirm(Vector2 pos)
    {
        isConfirm = true;
        this.pos = pos;
    }

    public void Init()
    {
        isCancel = false;
        isConfirm = false;

        input.SwitchCurrentActionMap("Move");
        uiPrinter.PrintPosSelectorUI();

        cameraChanger.ChangeToSelectPhazeCamera();
        cameraZoomController = cameraChanger.GetZoomController();
    }

    public async UniTask<bool> MovePosSelect(int id)
    {
        Init();

        ActionSettable pawn = strage.GetPawnById<ActionSettable>(id);

        Vector3 cameraPos = pawn.VirtualPos;
        cameraPos.z = -1;

        var obj1 = container.InstantiatePrefab(posSelector);
        obj1.transform.position = cameraPos;

        var obj2 = container.InstantiatePrefab(posSelectorRangeCircle) ;
        obj2.transform.position = pawn.VirtualPos;

        PosSelectorRangeSetter setter = obj1.GetComponent<PosSelectorRangeSetter>();
        setter.Range = pawn.range;

        IRangeCircleScaler scaler = obj2.GetComponent<IRangeCircleScaler>();
        scaler.SetRadius(pawn.range);

        cameraZoomController.OrthoSize = pawn.range;

        await UniTask.WaitUntil(() => (isCancel || isConfirm), PlayerLoopTiming.Update, cts); 

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
        cts = this.GetCancellationTokenOnDestroy();
        input = GetComponent<PlayerInput>();
    }
}
