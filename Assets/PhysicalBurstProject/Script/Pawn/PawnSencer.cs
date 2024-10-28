using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering;

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

    private void SetRange(float r)
    {
        transform.localScale = new Vector3(r, r, 1);
        range = r;
    }
         
    private void Awake()
    {
        senced = false;
        sencedTarget = default;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        AttackAble target = collision.transform.root.GetComponent<AttackAble>();
        if (target == null) return;

        if(target.IsMove && target.Type == senceType)
        {
            senced = true;
            sencedTarget = target;
        }
    }

    public void Delete() => Destroy(gameObject);

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}