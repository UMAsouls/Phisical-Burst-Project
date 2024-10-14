using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICmdSelectUIPrinter{
    public void PrintActionSelecter();

    public ICmdSelectorController PrintCmdSelecter(string[] cmdList);

    public void DestroyCmdSelector();

    
}
