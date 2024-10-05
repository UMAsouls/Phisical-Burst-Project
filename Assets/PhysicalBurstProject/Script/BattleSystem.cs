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

    private SpeedGettable[] pawns;

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
            SpeedGettable sx = (SpeedGettable)x;
            SpeedGettable sy = (SpeedGettable)y;

            return (new CaseInsensitiveComparer()).Compare(sy.speed, sx.speed);
        }
    }


    public void CommandConfirm(int index)
    {
        isConfirm = true;
        cmdIndex = index;
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
                Debug.Log("Select faze");
                uiPrinter.PrintPlayerInformation(p.ID);

                await Select(p);
            }
        }
        

        return;
    }

    private async UniTask BattleStart()
    {
        await UniTask.WaitUntil(() => pawnGettable.IsSetComplete);

        pawns = pawnGettable.GetPawnList<SpeedGettable>();
        Debug.Log("pawn get");

        isBattleEnd = false;

        return;
    }

    private async UniTask TurnStart()
    {
        System.Array.Sort(pawns, new SpeedComparer());
        return;
    }

    private async UniTask Select(SpeedGettable pawn)
    {
        isConfirm = false;
        uiPrinter.PrintCmdSelecter(defaultActions);

        await UniTask.WaitUntil(() => isConfirm);

        if (cmdIndex == 0) await MovePosSelect(pawn);

        return;
    }

    private async UniTask MovePosSelect(SpeedGettable pawn)
    {

        uiPrinter.DestroyPlayerInformation();
        uiPrinter.DestroyCmdSelector();

        isConfirm = false;

        isConfirm = await MovePosSelectable.MovePosSelect(pawn.ID);

        if (!isConfirm)
        {
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
