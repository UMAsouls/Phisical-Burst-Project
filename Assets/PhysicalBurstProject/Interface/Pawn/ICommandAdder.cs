

using System.Collections.Generic;

public interface ICommandAdder
{
    public AddCommand GetCommandList(string name);
    void AddCommand(string name, int cmd_idx, CommandType type);
}
