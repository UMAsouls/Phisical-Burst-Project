using System.Collections.Generic;
using UnityEngine;

public class StandardUIPrinter : UIPrinter, IStandardUIPritner
{

    [SerializeField]
    SerializedDictionary<GameObject> ui;

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

    public void PrintUIWorldPoint(string name, Vector3 point)
    {
        var p = Camera.main.WorldToScreenPoint(point);

        if (printedUI.ContainsKey(name)) DestroyUI(name);

        printedUI[name] = PrintUIAsChildAt(ui[name], p);
    }
}