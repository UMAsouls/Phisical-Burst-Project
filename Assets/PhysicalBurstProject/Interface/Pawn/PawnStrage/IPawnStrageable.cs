using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPawnStrageable
{
    public void AddPawnObj(GameObject obj);

    public bool IsSetComplete { set; }
}
