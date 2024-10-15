using System.Collections;
using UnityEngine;

public interface IActionCommand: ICommand
{
    public ActionCmdType Type { get; }

    public T GetMySelf<T>();
}