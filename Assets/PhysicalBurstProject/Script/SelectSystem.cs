using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BattleSystem : MonoBehaviour
{
    private ICmdSelectable[] porns;

    [Inject]
    private ICmdUIPrinter uiPrinter;

    private bool isConfirm;

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
        uiPrinter.PrintActionSelecter();

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
