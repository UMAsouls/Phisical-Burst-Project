using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PornInfoUI : MonoBehaviour, IPornInfoUI
{
    
    public string Name { set => nameTag.Name = value; }

    private GageSetable[] HPbars;
    private NameSetable nameTag;

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
        nameTag = GetComponentInChildren<NameSetable>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
