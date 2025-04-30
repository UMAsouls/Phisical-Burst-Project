using UnityEngine;

public interface ICommandStrage
{
    public IActionCommand GetActionCommand(string key);
    public IBattleCommand GetBattleCommand(string key);
    public IActionCommand[] GetActCmds(string[] keys);
    public IBattleCommand[] GetBattleCmds(string[] keys);
    public IActionCommand[] GetActCmds(CommandPackage[] keys);
    public IBattleCommand[] GetBattleCmds(CommandPackage[] keys);
}
