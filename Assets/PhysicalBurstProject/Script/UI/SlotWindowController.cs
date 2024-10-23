using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SlotWindowController : MonoBehaviour, SlotWindowControlable
{

    [SerializeField]
    private GameObject[] Slots;

    private ISlotTextSettable[] slotSetters;
    private RectTransform[] slotRects;
    private SlotUIAnimControl[] slotAnims;

    [SerializeField]
    private float fontSize;

    private float MaxWidth;

    private float firstWidth;

    private RectTransform selfRect;

    public void ActionSet(string action, int idx)
    {
        Debug.Log("actset: " + action);
        for (int i = 0; i < Slots.Length; i++)
        {
            if (i == idx) slotSetters[i].Text = action;
        }
        SizeUpdate();
    }

    private void SizeUpdate()
    {
        MaxWidth = -1;
        for (int i = 0; i < Slots.Length; i++)
        {
            slotSetters[i].FontSize = fontSize;
            slotSetters[i].SizeUpdate();
            if (MaxWidth < slotSetters[i].TextWidth) MaxWidth = slotSetters[i].TextWidth;
        }

        if (MaxWidth < firstWidth) MaxWidth = firstWidth;

        foreach (var rect in slotRects) rect.sizeDelta = new Vector2(0, rect.sizeDelta.y);

        selfRect.sizeDelta = new Vector2(MaxWidth + 20, slotRects[0].sizeDelta.y * Slots.Length + 10);
    }

    private void Awake()
    {
        slotSetters = new ISlotTextSettable[Slots.Length];
        slotRects = new RectTransform[Slots.Length];
        slotAnims = new SlotUIAnimControl[Slots.Length];

        for (int i = 0; i < Slots.Length; i++)
        {
            slotSetters[i] = Slots[i].GetComponent<ISlotTextSettable>();
            slotRects[i] = Slots[i].GetComponent<RectTransform>();
            slotAnims[i] = Slots[i].GetComponent<SlotUIAnimControl>();
        }

        selfRect = GetComponent<RectTransform>();
        firstWidth = selfRect.sizeDelta.x - 20;

    }

    // Use this for initialization
    void Start()
    {
        SizeUpdate();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void BurstAnim(int idx)
    {
        slotAnims[idx].BurstAnimation();
    }

    public void FocusAnim(int idx)
    {
        slotAnims[idx].FocusAnimation();
    }

    public void BlueFocusAnim(int idx)
    {
        slotAnims[idx].BlueFocusAnimation();
    }

    public void YellowFocusAnim(int idx)
    {
        slotAnims[idx].YellowFocusAnimation();
    }

    public void AnimEnd(int idx)
    {
        slotAnims[idx].EndAnimation();
    }
}