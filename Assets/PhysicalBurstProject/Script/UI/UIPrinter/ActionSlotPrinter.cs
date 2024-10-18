using System.Collections;
using UnityEngine;

public class ActionSlotPrinter : UIPrinter, IActionSlotPrinter
{

    [SerializeField]
    private GameObject actionSlot;

    [SerializeField]
    private Vector2 pos;

    private GameObject printedSlot;

    public void DestroyActionSlot()
    {
        if(printedSlot != null)
        {
            Destroy(printedSlot);
            printedSlot = null;
        }
    }

    public ActionSlotControlable PrintActionSlot()
    {
        if (printedSlot != null) DestroyActionSlot();

            printedSlot = PrintUIAsChildAt(actionSlot, pos);

        return printedSlot.GetComponent<ActionSlotControlable>();
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