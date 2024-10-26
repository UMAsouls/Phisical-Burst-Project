using System.Collections;
using UnityEngine;

public class EffectUnit : MonoBehaviour
{

    [SerializeField]
    private GameObject BurstEffect;

    public void Burst()
    {
        Instantiate(BurstEffect, transform.position, Quaternion.identity);
    }

    public void Damage(int damage)
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
}