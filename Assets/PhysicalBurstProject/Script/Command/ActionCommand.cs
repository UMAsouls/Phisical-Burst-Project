using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionCommand : IActionCommand
{
    private string name;
    public string Name => name;

    public ActionCmdType Type => throw new System.NotImplementedException();
}
