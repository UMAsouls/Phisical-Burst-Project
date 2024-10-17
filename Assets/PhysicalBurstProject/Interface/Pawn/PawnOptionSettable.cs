using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface PawnOptionSettable 
{
    public IStatus Status { set; }

    public IActionCommand[] ActionCommands { set; }

    public int ID { set; }
}
