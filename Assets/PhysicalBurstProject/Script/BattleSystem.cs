using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BattleSystem : MonoBehaviour,CmdConfirmAble
{
    private string[] defaultActions =
    {
        "ˆÚ“®", "PŒ‚", "‘Ò‚¿•š‚¹", "s“®"
    };

    private ICmdSelectable[] porns;

    [Inject]
    private IBattleUIPrinter uiPrinter;

    private bool isConfirm;

    private int cmdIndex;

    public void CommandConfirm(int index)
    {
        isConfirm = true;
        cmdIndex = index;
    }

    private async UniTask Battle()
    {
        foreach (var p in porns)
        {
            uiPrinter.PrintPlayerInformation(p);
            
            await Select(p);
        }

        
    }

    private async UniTask Select(ICmdSelectable porn)
    {
        uiPrinter.PrintCmdSelecter(defaultActions);

        await UniTask.WaitUntil(() => isConfirm);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
