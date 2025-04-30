using Cysharp.Threading.Tasks;
using System.Collections;
using UnityEngine;

public class BattleStartEndUIPrinter : UIPrinter, IObserver<EffectTiming>
{

    [SerializeField]
    GameObject startUI;

    [SerializeField]
    GameObject winUI;

    [SerializeField]
    GameObject loseUI;

    private GameObject printedUI;

    private bool EffectEnd;

    private async UniTask PrintUIAndWait(GameObject ui)
    {
        EffectEnd = false;
        printedUI = PrintUIAsChild(ui);
        printedUI.GetComponent<IObservable<EffectTiming>>().Subscribe(this);

        await UniTask.WaitUntil(() => EffectEnd, cancellationToken: destroyCancellationToken);
    }

    public async UniTask PrintStartUIAndWait()
    {
        await PrintUIAndWait(startUI);
    }

    public async UniTask PrintWinUIAndWait()
    {
        await PrintUIAndWait(winUI);
    }

    public async UniTask PrintLoseUIAndWait()
    {
        await PrintUIAndWait(loseUI);
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnComplete()
    {
        throw new System.NotImplementedException();
    }

    public void OnNext(EffectTiming value)
    {
        if(value == EffectTiming.EffectEnd) EffectEnd = true;
    }
}