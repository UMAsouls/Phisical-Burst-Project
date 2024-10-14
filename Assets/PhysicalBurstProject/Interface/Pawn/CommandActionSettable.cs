using UnityEngine;

public interface CommandActionSettable : ActionSettable
{
    ICommand[] GetActionCommands();
}
