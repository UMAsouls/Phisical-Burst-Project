using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IRangeAttackCommand : IActionCommand
{
    public float Range { get; }

    public float Damage { get; }
}
