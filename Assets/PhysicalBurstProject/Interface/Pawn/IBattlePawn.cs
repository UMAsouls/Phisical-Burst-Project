using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IBattlePawn
{
    public IPawnActionManager ActionManager { get; }
    public IStatus Status { get; }
    public IVirtualPawn VirtualPawn { get; }
}
