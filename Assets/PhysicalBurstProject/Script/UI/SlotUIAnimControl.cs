using System.Collections;
using UnityEngine;

public class SlotUIAnimControl : MonoBehaviour
{

    private Animator animator;

    private bool isAnimation = false;

    private void AnimInit()
    {
        if(isAnimation) EndAnimation();
        isAnimation = true;
    }

    public void BurstAnimation()
    {
        AnimInit();
        animator.SetTrigger("Burst");
    }

    public void FocusAnimation()
    {
        AnimInit();
        animator.SetTrigger("Focus");
    }

    public void BlueFocusAnimation()
    {
        AnimInit();
        animator.SetTrigger("BlueFocus");
    }

    public void YellowFocusAnimation()
    {
        AnimInit();
        animator.SetTrigger("YellowFocus");
    }

    public void EndAnimation()
    {
        isAnimation = false;
        animator.SetTrigger("AnimEnd");
    }

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}