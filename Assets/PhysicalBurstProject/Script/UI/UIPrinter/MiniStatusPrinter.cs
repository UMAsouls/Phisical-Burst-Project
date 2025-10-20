using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MiniStatusPrinter : UIPrinter
{
    [Inject]
    IPawnGettable strage;

    [SerializeField]
    GameObject ui;

    Dictionary<int, GameObject> printedUI;

    public void PrintUI(int id, IStatus status)
    {
        if(printedUI.ContainsKey(id)) DestroyUI(id);
        printedUI[id] = PrintUIAsChild(ui);
        status.Subscribe(printedUI[id].GetComponent<MiniStatusBar>());
    }

    public void DestroyUI(int id)
    {
        if(printedUI.ContainsKey(id))
        {
            Destroy(printedUI[id]);
            printedUI.Remove(id);
        }
    }

    private void Awake()
    {
        printedUI = new Dictionary<int, GameObject>();
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