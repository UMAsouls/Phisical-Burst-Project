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

    [SerializeField, Multiline(3)]
    private string description;
    public string Description => description;

    private bool EffectEnd;

    public ActionCommand()
    {
        name = "";
        mana = 0f;
        burstRatio = 0f;
        selectPriority = 0f;
        description = "";
    }

    public ActionCommand(string name, float mana, float burstRatio, float selectPriority, string description)
    {
        this.name = name;
        this.mana = mana;
        this.burstRatio = burstRatio;
        this.selectPriority = selectPriority;
        this.description = description;
    }

    public ActionCommand(ActionCommand<V> cmd) : this(cmd.name, cmd.mana, cmd.burstRatio, cmd.selectPriority, cmd.description) { }



    public abstract float EffectScale { get; }

    public abstract string GetTypeText();

    public abstract IActionCommand Copy();

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
