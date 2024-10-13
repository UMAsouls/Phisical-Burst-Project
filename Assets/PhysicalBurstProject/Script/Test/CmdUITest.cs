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

        ui.CmdAdd("�e�X�g1");
        ui.CmdAdd("�e�X�g100");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
