using Cysharp.Threading.Tasks;
using System.Collections;
using UnityEngine;

public class BattleStartUIPrinter : UIPrinter, IObserver<EffectTiming>
{

    [SerializeField]
    GameObject ui;

    private GameObject printedUI;

    private bool EffectEnd;

    public async UniTask PrintUIAndWait()
    {
        printedUI = PrintUIAsChild(ui);
        printedUI.GetComponent<IObservable<EffectTiming>>().Subscribe(this);

        await UniTask.WaitUntil(() => EffectEnd, cancellationToken: destroyCancellationToken);
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