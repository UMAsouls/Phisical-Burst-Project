using System.Collections;
using UnityEngine;

public class CmdInfoUIPrinter : UIPrinter, ICmdInfoUIPrinter
{
    [SerializeField]
    GameObject ui;

    [SerializeField]
    Vector2 pos;

    private GameObject printedUI;

    public void PrintUI(string name, string type, string description, string useMana)
    {
        if (printedUI != null) DestroyUI();

        printedUI = PrintUIAsChildAt(ui, pos);
        CmdInfoWindow controller = printedUI.GetComponent<CmdInfoWindow>();
        controller.Name = name;
        controller.Type = type;
        controller.Description = description;
        controller.UseMana = useMana;
    }

    public void PrintUI(ICommand cmd)
    {
        PrintUI(cmd.Name, cmd.GetTypeText(), cmd.Description, cmd.UseMana.ToString());
    }

    public void DestroyUI()
    {
        if(printedUI != null)
        {
            Destroy(printedUI);
            printedUI = null;
        }
    }


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}