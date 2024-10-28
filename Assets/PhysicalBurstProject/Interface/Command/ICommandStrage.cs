using UnityEngine;

public interface ICommandStrage
{
    public IActionCommand GetActionCommand(string key);
    public IBattleCommand GetBattleCommand(string key);
}
