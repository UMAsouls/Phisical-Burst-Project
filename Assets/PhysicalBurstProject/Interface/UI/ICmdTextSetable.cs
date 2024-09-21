using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICmdTextSetable: StringSetable
{
    public float FontSize { set; get; }
}
