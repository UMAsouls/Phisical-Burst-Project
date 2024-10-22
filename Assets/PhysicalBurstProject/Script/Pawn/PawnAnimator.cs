using System.Collections;
using UnityEditor.Animations;
using UnityEngine;

public class PawnAnimator : MonoBehaviour, IPawnAnimator
{

    [SerializeField]
    private GameObject CharaObj;

    private Animator animator;

    private SpriteRenderer mat;

    string[] dirs = new string[] { "RIGHT", "LEFT", "UP", "DOWN" };

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

        foreach (var d in dirs) Debug.Log($"{d} : {animator.GetFloat(d)}");
    }

    public void EndMove()
    {
        animator.SetBool("Move", false);
        animator.SetTrigger("MoveEnd");

        foreach (string dir in dirs) animator.SetFloat(dir, 0);
    }

    private void Awake()
    {
        var obj = Instantiate(CharaObj);
        obj.transform.SetParent(transform);
        obj.transform.transform.localPosition = Vector3.zero;

        animator = obj.GetComponent<Animator>();
        mat = obj.GetComponent<SpriteRenderer>();
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