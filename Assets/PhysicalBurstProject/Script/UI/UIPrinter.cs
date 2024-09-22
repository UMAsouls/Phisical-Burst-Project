using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPrinter : IBattleUIPrinter, BattleUIPrinterSetter
{
    private IBattleUIPrinter battleUIPrinter;

    public IBattleUIPrinter BattleUIPrinter { set => battleUIPrinter = value; }

    public void PrintActionSelecter() => battleUIPrinter.PrintActionSelecter();

    public void PrintCmdSelecter(string[] cmdList) => battleUIPrinter.PrintCmdSelecter(cmdList);

    public void PrintPlayerInformation(IPawn pawn) => battleUIPrinter.PrintPlayerInformation(pawn);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
