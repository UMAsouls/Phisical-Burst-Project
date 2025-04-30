using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering;
using Zenject;

public class PawnSencer : MonoBehaviour, IPawnSencer
{
    private bool senced = false;
    public bool Senced => senced;

    private AttackAble sencedTarget;
    public AttackAble SencedTarget => sencedTarget;

    private PawnType senceType;
    public PawnType SenceType { set => senceType = value; }

    private float range;
    public float Range { get => range; set => SetRange(value); }

    [Inject]
    IPawnGettable strage;

    private void SetRange(float r)
    {
        transform.localScale = new Vector3(r*2, r*2, 1);
        range = r;
    }
         
    private void Awake()
    {
        senced = false;
        sencedTarget = default;
    }

    public void Delete() => Destroy(gameObject);

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        List<AttackAble> list = strage.GetPawnsInArea<AttackAble>(transform.position, Range);

        foreach (AttackAble target in list)
        {
            if (target == null) continue;

            if (target.IsMove && target.Type == senceType)
            {
                senced = true;
                sencedTarget = target;
            }
        }
    }
}