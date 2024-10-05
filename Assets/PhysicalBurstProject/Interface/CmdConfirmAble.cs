using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface CmdConfirmAble 
{
    public void CommandConfirm(int index);

    public void CmdCancel();
}
