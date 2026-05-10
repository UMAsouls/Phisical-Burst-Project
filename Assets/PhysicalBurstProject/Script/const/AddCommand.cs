using System;
using System.Collections.Generic;

public struct AddCommand
{
    public AddCommand(int a)
    {
        AddBattleCmdList = new List<int>();
        AddActionCmdList = new List<int>();
    }

    public List<int> AddBattleCmdList { get; }
    public List<int> AddActionCmdList { get; }

    public override string ToString()
    {
        string ans = "BattleCmd: ";
        foreach(var cmd in AddBattleCmdList) ans += cmd.ToString() + ", ";
        ans += "\nActionCmd: ";
        foreach (var cmd in AddActionCmdList) ans += cmd.ToString() + ", ";

        return ans;
    }
}
