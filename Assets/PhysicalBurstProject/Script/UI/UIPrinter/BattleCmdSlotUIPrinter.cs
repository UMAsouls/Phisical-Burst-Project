using System.Collections;
using System.Net.NetworkInformation;
using UnityEngine;

public class BattleCmdSlotUIPrinter : UIPrinter, IBattleCmdSlotUIPrinter
{
    [SerializeField]
    GameObject ui;

    [SerializeField]
    Vector2 pos;

    private GameObject printedUI;

    public void DestroyUI()
    {
        if(printedUI != null) Destroy(printedUI);
    }

    public SlotWindowControlable PrintUI()
    {
        if(printedUI != null)  DestroyUI();

        printedUI = PrintUIAsChildAt(ui, pos);

        return printedUI.GetComponent<SlotWindowControlable>();
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