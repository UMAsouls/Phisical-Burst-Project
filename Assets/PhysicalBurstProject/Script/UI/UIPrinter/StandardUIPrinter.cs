using System.Collections.Generic;
using UnityEngine;

public class StandardUIPrinter : UIPrinter, IStandardUIPritner
{

    [SerializeField]
    SerializedDictionary<string, GameObject> ui;

    private Dictionary<string, GameObject> printedUI = new Dictionary<string, GameObject>();

    public void DestroyUI(string name)
    {
        if (printedUI.ContainsKey(name))
        {
            Destroy(printedUI[name]);
            printedUI.Remove(name);
        }
    }

    public void PrintUI(string name)
    {
        if (printedUI.ContainsKey(name)) DestroyUI(name);

        printedUI[name] = PrintUIAsChild(ui[name]);
    }
}