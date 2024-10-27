using System.Collections;
using UnityEngine;
using Zenject;

public class EffectUnit : MonoBehaviour
{

    [SerializeField]
    private GameObject BurstEffect;

    [SerializeField]
    private GameObject StunEffect;

    [Inject]
    private IDamageHealUIPrinter DamageHealUIPrinter;

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
}