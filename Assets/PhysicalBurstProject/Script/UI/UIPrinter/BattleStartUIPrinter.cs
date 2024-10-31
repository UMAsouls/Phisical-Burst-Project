using Cysharp.Threading.Tasks;
using System.Collections;
using UnityEngine;

public class BattleStartUIPrinter : UIPrinter, IObserver<EffectTiming>
{

    [SerializeField]
    GameObject ui;

    private GameObject printedUI;

    public async UniTask PrintUIAndWait()
    {

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
        throw new System.NotImplementedException();
    }
}