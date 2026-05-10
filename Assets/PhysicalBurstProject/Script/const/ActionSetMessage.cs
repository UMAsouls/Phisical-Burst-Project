
using System;

using UnityEngine.InputSystem;


public struct ActionSetMessage
{
    public ActionSetMessage(
        InputMode inputMode, string action, 
        Action<InputAction.CallbackContext> func,
        bool remove = false
    )
    {
        Type = inputMode;
        Action = action;
        Function = func;
        Remove = remove;
    }

    public InputMode Type { get; }
    public string Action { get;  }
    public Action<InputAction.CallbackContext> Function { get;  }
    public bool Remove { get; }

    public override string ToString()
    {
        return $"Type: {Enum.GetName(typeof(InputMode), Type)}, Action: {Action}";
    }
}
