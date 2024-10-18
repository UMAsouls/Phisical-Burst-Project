using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ActionSlotController : MonoBehaviour, ActionSlotControlable
{

    [SerializeField]
    private GameObject[] Slots;

    private IActionTextSettable[] slotSetters;
    private RectTransform[] slotRects;

    [SerializeField]
    private float fontSize;

    private float MaxWidth;

    private float firstWidth;

    private RectTransform selfRect;

    public void ActionSet(string action, int idx)
    {
        Debug.Log("actset: " + action);
        MaxWidth = -1;
        for (int i = 0; i < Slots.Length; i++)
        {
            slotSetters[i].FontSize = fontSize;
            if (i == idx) slotSetters[i].Text = action;
            else slotSetters[i].SizeUpdate();

            if(MaxWidth < slotRects[i].sizeDelta.x) MaxWidth = slotRects[i].sizeDelta.x;
        }

        if(MaxWidth < firstWidth) MaxWidth = firstWidth;

        foreach (var rect in slotRects)
        {
            rect.sizeDelta = new Vector2(MaxWidth, rect.sizeDelta.y);
        }

        selfRect.sizeDelta = new Vector2 (MaxWidth+20, slotRects[0].sizeDelta.y*2 + 10);


    }

    private void Awake()
    {
        slotSetters = new IActionTextSettable[Slots.Length];
        slotRects = new RectTransform[Slots.Length];

        for (int i = 0; i < Slots.Length; i++)
        {
            slotSetters[i] = Slots[i].GetComponent<IActionTextSettable>();
            slotRects[i] = Slots[i].GetComponent<RectTransform>();
        }

        selfRect = GetComponent<RectTransform>();
        firstWidth = selfRect.sizeDelta.x - 20;

        Debug.Log("setComplete");
        Debug.Log(slotSetters[0]);

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