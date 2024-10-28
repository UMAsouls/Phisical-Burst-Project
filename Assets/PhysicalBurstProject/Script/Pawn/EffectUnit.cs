using Cysharp.Threading.Tasks;
using System.Collections;
using UnityEngine;
using Zenject;

public class EffectUnit : MonoBehaviour, IObserver<EffectTiming>
{

    [SerializeField]
    private GameObject BurstEffect;

    [SerializeField]
    private GameObject StunEffect;

    [SerializeField]
    private GameObject AmbushEffect;

    [SerializeField]
    private Vector2 AmbushPos;

    [Inject]
    private IDamageHealUIPrinter DamageHealUIPrinter;

    private bool EffectEnd;

    public async UniTask Ambush()
    {
        EffectEnd = false;
        var obj = Instantiate(AmbushEffect);
        obj.GetComponent<IObservable<EffectTiming>>().Subscribe(this);

        await UniTask.WaitUntil(() => EffectEnd, cancellationToken: destroyCancellationToken);
    }

    public void Burst()
    {
        Instantiate(BurstEffect, transform.position, Quaternion.identity);
    }

    public void Stun()
    {
        Instantiate(StunEffect, transform.position, Quaternion.identity);
    }

    public void Damage(int damage)
    {
        DamageHealUIPrinter.PrintDamage(transform.position, damage);
    }

    public void Heal(int heal)
    {
        DamageHealUIPrinter.PrintHeal(transform.position, heal);
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