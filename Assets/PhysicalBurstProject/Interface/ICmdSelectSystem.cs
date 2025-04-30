using Cysharp.Threading.Tasks;
using System.Collections;
using UnityEngine;

public interface ICmdSelectSystem
{
    public UniTask<bool> CmdSelect(int id);
}