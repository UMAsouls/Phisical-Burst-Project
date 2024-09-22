using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CmdUITest : MonoBehaviour
{

    [SerializeField]
    GameObject obj;

    ICmdUI ui;

    private void Awake()
    {
        ui = obj.GetComponent<ICmdUI>();
    }

    // Start is called before the first frame update
    void Start()
    {

        ui.CmdAdd("テスト1");
        ui.CmdAdd("テスト100");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
