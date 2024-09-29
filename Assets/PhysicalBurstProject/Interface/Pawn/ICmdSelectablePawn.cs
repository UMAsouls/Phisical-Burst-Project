using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICmdSelectablePawn : IPawn
{
    ICommand[] GetCommands();

    void Action();
}
