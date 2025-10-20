
using UnityEngine;
using Zenject;

public class PawnAnimator : MonoBehaviour, IPawnAnimator, IObserver<StatusFrag>, IObserver<TurnPhaseFrag>
{

    [SerializeField]
    private GameObject CharaObj;

    private Animator animator;

    private EmoteUnit emoteUnit;

    private Material mat;

    [Inject]
    private IObservable<TurnPhaseFrag> turnPhaseObservable;

    string[] dirs = new string[] { "RIGHT", "LEFT", "UP", "DOWN" };

    public void AttackEmote(Vector2 dir)
    {
        emoteUnit.AttackEmote(dir);
    }

    public void DodgeEmote(Vector2 dis) => emoteUnit.DodgeEffect(dis);

    public void MoveAnimation(Vector2 dir)
    {
        if(dir.x > 0)
        {
            animator.SetFloat("RIGHT", dir.x);
            animator.SetFloat("LEFT", 0);
        } else
        {
            animator.SetFloat("RIGHT", 0);
            animator.SetFloat("LEFT", -1*dir.x);
        }

        if(dir.y > 0)
        {
            animator.SetFloat("UP", dir.y);
            animator.SetFloat("DOWN", 0);
        }else
        {
            animator.SetFloat("UP", 0);
            animator.SetFloat("DOWN", -1*dir.y);
        }

        animator.SetBool("Move", true);
        animator.SetTrigger("MoveStart");
    }

    public void EndMove()
    {
        animator.SetBool("Move", false);
        animator.SetTrigger("MoveEnd");

        foreach (string dir in dirs) animator.SetFloat(dir, 0);
    }

    public void Death()
    {
        animator.SetTrigger("DEATH");
    }

    public void ChangeNormal()
    {
        mat.SetFloat("_Stun", 0);
        mat.SetFloat("_Burst", 0);
    }

    public void ChangeBurst()
    {
        ChangeNormal();
        mat.SetFloat("_Burst", 1);
    }

    public void ChangeStun()
    {
        ChangeNormal();
        mat.SetFloat("_Stun", 1);
    }

    private void Awake()
    {
        var obj = Instantiate(CharaObj);
        obj.transform.SetParent(transform);
        obj.transform.transform.localPosition = Vector3.zero;

        animator = obj.GetComponent<Animator>();
        mat = obj.GetComponent<SpriteRenderer>().material;
        emoteUnit = obj.GetComponent<EmoteUnit>();
    }

    // Use this for initialization
    void Start()
    {
        turnPhaseObservable.Subscribe(this);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnComplete()
    {
        throw new System.NotImplementedException();
    }

    public void OnNext(StatusFrag value)
    {
        switch(value)
        {
            case(StatusFrag.Burst):
                ChangeBurst();
                break;
            case(StatusFrag.Stun):
                ChangeStun(); 
                break;
            default:
                ChangeNormal();
                break;
        }
    }

    public void OnNext(TurnPhaseFrag value)
    {
        switch (value)
        {
            case (TurnPhaseFrag.TurnEnd):
                ChangeNormal();
                break;
        }

    }
}