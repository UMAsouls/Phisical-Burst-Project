using System.Collections;
using UnityEngine;

public interface IActionCommand: ICommand
{
    public ActionCmdType Type { get; }
}