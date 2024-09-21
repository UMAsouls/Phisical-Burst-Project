using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICmdSelectable : IPawn
{
    ICommand[] GetCommands();
}