using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;

public interface IActionCommand: ICommand
{
    public ActionCmdType Type { get; }

    public T GetMySelf<T>();

    public IActionCommand Copy();

    public float EffectScale { get; }
}