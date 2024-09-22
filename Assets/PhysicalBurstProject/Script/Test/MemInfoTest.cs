using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemInfoTest : MonoBehaviour
{

    [SerializeField]
    GameObject obj;

    IPornInfoUI ui;

    // Start is called before the first frame update
    void Start()
    {
        ui = obj.GetComponent<IPornInfoUI>();
        ui.SetHP(100, 50);
        ui.Name = "Test";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
