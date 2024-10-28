using System.Collections.Generic;
using UnityEngine;

public class MagicCircle : MonoBehaviour, IObservable<EffectTiming>
{
    List<IObserver<EffectTiming>> observers;

    public void Subscribe(IObserver<EffectTiming> observer)
    {
        observers.Add(observer);
    }

    public void End()
    {
        foreach (var observer in observers)
        {
            observer.OnNext(EffectTiming.EffectEnd);
        }
        Destroy(gameObject);
    }

    private void Awake()
    {
        observers = new List<IObserver<EffectTiming>>();
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}