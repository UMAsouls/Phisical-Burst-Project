using System.Collections;
using UnityEngine;

public class ResultUIPrinter : UIPrinter
{

    [SerializeField]
    GameObject WinUI;

    [SerializeField]
    GameObject LoseUI;

    GameObject printedUI;

    public void PrintWinResult(int num, string next)
    {
        printedUI = PrintUIAsChild(WinUI);
        printedUI.GetComponent<ResultUI>().TurnNum = num;
        printedUI.GetComponent<ResultUI>().NextScene = next;
    }

    public void PrintLoseResult(int num, string next)
    {
        printedUI = PrintUIAsChild(LoseUI);
        printedUI.GetComponent<ResultUI>().TurnNum = num;
        printedUI.GetComponent<ResultUI>().NextScene = next;
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