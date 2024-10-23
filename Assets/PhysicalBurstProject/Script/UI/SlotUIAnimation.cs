using System.Collections;
using UnityEngine;

public class SlotUIAnimation : MonoBehaviour
{

    private SlotFrameAnimator m_SlotFrameAnimator;

    public void SetIdle()
    {
        m_SlotFrameAnimator.SetBurst(false);
        m_SlotFrameAnimator.SetFocus(false);
    }

    public void SetBurst()
    {
        m_SlotFrameAnimator.SetBurst(true);
    }

    public void SetFocus()
    {
        m_SlotFrameAnimator.SetFocus(true);
    }

    public void TurnBlue(float v)
    {
        m_SlotFrameAnimator.TurnBlue(v);
    }

    public void TurnYellow(float v)
    {
        m_SlotFrameAnimator.TurnYellow(v);
    }

    // Use this for initialization
    void Start()
    {
        m_SlotFrameAnimator = GetComponentInChildren<SlotFrameAnimator>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}