using Cysharp.Threading.Tasks;
using System.Collections;
using UnityEngine;

public interface ICmdSelectSystem
{
    public UniTask CmdSelect(int id);
}