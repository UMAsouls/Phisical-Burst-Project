using PlasticPipe.PlasticProtocol.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public struct AddCommand
{
    public AddCommand(int a)
    {
        AddBattleCmdList = new List<int>();
        AddActionCmdList = new List<int>();
    }

    public List<int> AddBattleCmdList { get; }
    public List<int> AddActionCmdList { get; }
}
