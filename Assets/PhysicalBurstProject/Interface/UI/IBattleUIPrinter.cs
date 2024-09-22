using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBattleUIPrinter{
    public void PrintActionSelecter();

    public void PrintPlayerInformation(IPawn pawn);
}
