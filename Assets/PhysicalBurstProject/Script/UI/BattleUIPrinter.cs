using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BattleUIPrinter : UIPrinter,ICmdSelectUIPrinter,IPlayerInformationUIPrinter
{
    

    [Inject]
    IPawnGettable pawnStrage;

    [SerializeField]
    private GameObject CmdWindow;

    [SerializeField]
    private Vector2 CmdWindowPos;

    [SerializeField]
    private GameObject PawnInfo;

    [SerializeField]
    private Vector2 PawnInfoPos;


    private GameObject printedCmdWindow;

    private GameObject printedPawnInfo;

    public void DestroyCmdSelector()
    {
        if (printedCmdWindow != null)
        {
            Destroy(printedCmdWindow);
        }
    }

    public void DestroyPlayerInformation()
    {
        if (printedPawnInfo != null) { Destroy(printedPawnInfo); }
    }

    public void PrintActionSelecter()
    {
        printedCmdWindow = PrintUIAsChildAt(CmdWindow, CmdWindowPos);

        ICmdUI ui = printedCmdWindow.GetComponent<ICmdUI>();
        ui.CmdAdd("èPåÇ");
        ui.CmdAdd("ë“ÇøïöÇπ");
        ui.CmdAdd("à⁄ìÆ");
        ui.CmdAdd("çsìÆ");
    }

    public ICmdSelectorController PrintCmdSelecter(string[] cmdList)
    {
        DestroyCmdSelector();

        var obj = PrintUIAsChildAt(CmdWindow, CmdWindowPos);

        ICmdUI ui = obj.GetComponent <ICmdUI>();
        foreach (string cmd in cmdList)
        {
            ui.CmdAdd(cmd);
        }

        printedCmdWindow = obj;
        return obj.GetComponent<ICmdSelectorController>();
    }

    public void PrintPlayerInformation(int id)
    {
        IPawn pawn = pawnStrage.GetPawnById<IPawn>(id);

        DestroyPlayerInformation();

        var obj = PrintUIAsChildAt(PawnInfo, PawnInfoPos);

        IPawnInfoUI ui = obj.GetComponent<IPawnInfoUI>();
        ui.Name = pawn.Name;
        ui.SetHP(pawn.MaxHP, pawn.HP);

        printedPawnInfo = obj;
    }

    private void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
