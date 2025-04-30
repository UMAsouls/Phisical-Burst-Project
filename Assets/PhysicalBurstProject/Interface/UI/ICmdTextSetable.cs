using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICmdTextSetable: StringSetable
{
    public new string Text { get; set; }
    public float FontSize { set; get; }
    public float TextWidth { get; }
    public float TextSpace { get; set; }

}
