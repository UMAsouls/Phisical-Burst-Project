using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class StrongAttackCommand : IBattleCommand
{
    private string name;

    public BattleCommandType Type => BattleCommandType.Strong;

    public string Name => name;

    public float UseMana => throw new NotImplementedException();
}
