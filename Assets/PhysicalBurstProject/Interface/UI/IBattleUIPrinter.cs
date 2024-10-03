using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBattleUIPrinter{
    public void PrintActionSelecter();

    public void PrintCmdSelecter(string[] cmdList);

    public void PrintPlayerInformation(IPawn pawn);

    public void DestroyCmdSelector();

    public void DestroyPlayerInformation();
}
