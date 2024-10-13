using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Triggers;
using ModestTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BattleSystem : MonoBehaviour, CmdConfirmAble
{
    private string[] defaultActions =
    {
        "移動", "襲撃", "待ち伏せ", "行動"
    };

    private ActionSelectable[] pawns;

    [Inject]
    private IBattleUIPrinter uiPrinter;

    [Inject]
    private IPawnGettable pawnGettable;

    [Inject]
    private MovePosSelectable MovePosSelectable;

    private bool isConfirm;
    private bool isCancel;

    private int cmdIndex;

    private bool isBattleEnd;

    /// <summary>
    /// SpeedGettable[]のCoparer
    /// 降順にソート
    /// </summary>
    class SpeedComparer : IComparer
    {
        public int Compare(object x, object y)
        {
            ActionSelectable sx = (ActionSelectable)x;
            ActionSelectable sy = (ActionSelectable)y;

            return (new CaseInsensitiveComparer()).Compare(sy.speed, sx.speed);
        }
    }


    public void CommandConfirm(int index)
    {
        isConfirm = true;
        cmdIndex = index;
    }

    public void CmdCancel()
    {
        isCancel = true;
    }

    private async UniTask Battle()
    {
        Debug.Log("BattleStart");
        await BattleStart();

        while (!isBattleEnd)
        {
            await TurnStart();

            foreach (var p in pawns)
            {
                while (p.ActPoint > 0)
                {
                    Debug.Log("Select faze");
                    uiPrinter.PrintPlayerInformation(p.ID);

                    do
                    {
                        isCancel = false;
                        await Select(p);
                        if (isCancel)
                        {
                            if(p.CancelSelect()) uiPrinter.DestroyCmdSelector();
                        }
                    } while (isCancel);
                }  
            }
            await TurnEnd();
        }
        return;
    }

    private async UniTask BattleStart()
    {
        await UniTask.WaitUntil(() => pawnGettable.IsSetComplete);

        pawns = pawnGettable.GetPawnList<ActionSelectable>();
        Debug.Log("pawn get");

        isBattleEnd = false;

        return;
    }

    private async UniTask TurnStart()
    {
        System.Array.Sort(pawns, new SpeedComparer());
        foreach (var p in pawns) await p.TurnStart();
        return;
    }

    private async UniTask Select(ActionSelectable pawn)
    {
        isConfirm = false;
        do
        {
            isCancel = false;
            uiPrinter.PrintCmdSelecter(defaultActions);

            await UniTask.WaitUntil(() => isConfirm | isCancel);

            if (isCancel) break;

            if (cmdIndex == 0) await MovePosSelect(pawn);
        }while(isCancel);

        return;
    }

    private async UniTask TurnEnd()
    {
        foreach (var p in pawns) await p.TurnEnd();
        return;
    }

    private async UniTask MovePosSelect(ActionSelectable pawn)
    {

        uiPrinter.DestroyPlayerInformation();
        uiPrinter.DestroyCmdSelector();

        isConfirm = false;

        isConfirm = await MovePosSelectable.MovePosSelect(pawn.ID);

        if (!isConfirm)
        {
            uiPrinter.PrintPlayerInformation(pawn.ID);
            isCancel = true;
            return;
        }
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
