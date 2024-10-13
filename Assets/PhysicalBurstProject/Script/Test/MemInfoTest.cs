using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemInfoTest : MonoBehaviour
{

    [SerializeField]
    GameObject obj;

    IPawnInfoUI ui;

    // Start is called before the first frame update
    void Start()
    {
        ui = obj.GetComponent<IPawnInfoUI>();
        ui.SetHP(100, 50);
        ui.Name = "Test";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
