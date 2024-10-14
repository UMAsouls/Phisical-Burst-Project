using UnityEngine;

public interface CommandActionSettable : ActionSettable
{
    IActionCommand[] GetActionCommands();
}
