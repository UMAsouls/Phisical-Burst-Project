using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class ActionCommand<V> : IActionCommand, IObserver<EffectTiming>
{
    [SerializeField]
    protected string name;
    public string Name => name;

    public abstract ActionCmdType Type { get; }

    [SerializeField]
    protected float mana;
    public float UseMana => mana;

    [SerializeField]
    protected float burstRatio;
    public float BurstRatio => burstRatio;

    [SerializeField]
    [Range(0, 100f)]
    private float selectPriority;
    public float SelectPriority { get => selectPriority; set => selectPriority = value; }

    [SerializeField, TextArea(18, 4)]
    private string description;
    public string Description => description;

    private bool EffectEnd;

    public abstract float EffectScale { get; }

    public abstract string GetTypeText();

    public T GetMySelf<T>()
    {
        if (typeof(T) == typeof(V)) return (T)(object)this;
        else return default(T);
    }

    protected async UniTask WaitEffect(Vector2 pos, GameObject effect, float scale = 1)
    {
        EffectEnd = false;
        var obj = MonoBehaviour.Instantiate(effect, pos, Quaternion.identity);
        obj.transform.localScale = Vector3.one * scale;

        IObservable<EffectTiming> comp = obj.GetComponent<IObservable<EffectTiming>>();
        comp.Subscribe(this);

        await UniTask.WaitUntil(() => (EffectEnd));
    }

    public void OnComplete()
    {
    }

    public void OnNext(EffectTiming value)
    {
        if (value == EffectTiming.EffectEnd) EffectEnd = true;
    }
}
