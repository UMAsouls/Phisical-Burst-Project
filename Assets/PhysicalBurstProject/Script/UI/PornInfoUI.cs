using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PornInfoUI : MonoBehaviour, IPornInfoUI
{
    
    public string Name { set => nameTag.Text = value; }

    private GageSetable[] HPbars;
    private ICmdTextSetable nameTag;

    public void SetHP(int MaxHP, int HP)
    {
        foreach (var hp in HPbars)
        {
            hp.Set(MaxHP, HP);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        HPbars = GetComponentsInChildren<GageSetable>();
        nameTag = GetComponentInChildren<ICmdTextSetable>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
