using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EffectEndObserver: MonoBehaviour, IObservable<EffectTiming>
{

    List<IObserver<EffectTiming>> list;

    [SerializeField]
    private float EndTime;

    public void Subscribe(IObserver<EffectTiming> observer)
    {
        list.Add(observer);
    }

    private async UniTask Count(CancellationToken token)
    {
        while(!token.IsCancellationRequested && EndTime > 0f)
        {
            await UniTask.Yield(PlayerLoopTiming.Update, token);
            EndTime -= Time.deltaTime;
        }
        BroadCast();
    }

    private void BroadCast()
    {
        foreach (var item in list)
        {
            if (item != null) item.OnNext(EffectTiming.EffectEnd);
        }
    }

    private void OnDestroy()
    {
        BroadCast();
    }

    private async void Awake()
    {
        list = new List<IObserver<EffectTiming>>();
        await Count(destroyCancellationToken);
    }
}
