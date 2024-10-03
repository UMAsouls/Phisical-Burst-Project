using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BattleUIPrinter : MonoBehaviour,IBattleUIPrinter
{
    [Inject]
    DiContainer diContainer;

    [SerializeField]
    private GameObject CmdWindow;

    [SerializeField]
    private Vector2 CmdWindowPos;

    [SerializeField]
    private GameObject PawnInfo;

    [SerializeField]
    private Vector2 PawnInfoPos;


    private GameObject printedCmdWindow;

    private GameObject printedPawnInfo;

    public void DestroyCmdSelector()
    {
        if (printedCmdWindow != null)
        {
            Destroy(printedCmdWindow);
        }
    }

    public void DestroyPlayerInformation()
    {
        if (printedPawnInfo != null) { Destroy(printedPawnInfo); }
    }

    public void PrintActionSelecter()
    {
        var obj = Instantiate(CmdWindow);
        obj.transform.SetParent(transform, false);

        RectTransform rect = obj.GetComponent<RectTransform>();
        rect.anchoredPosition = CmdWindowPos;

        ICmdUI ui = obj.GetComponent<ICmdUI>();
        ui.CmdAdd("èPåÇ");
        ui.CmdAdd("ë“ÇøïöÇπ");
        ui.CmdAdd("à⁄ìÆ");
        ui.CmdAdd("çsìÆ");
    }

    public void PrintCmdSelecter(string[] cmdList)
    {
        DestroyCmdSelector();

        var obj = diContainer.InstantiatePrefab(CmdWindow);
        obj.transform.SetParent(transform, false);

        RectTransform rect = obj.GetComponent<RectTransform>();
        rect.anchoredPosition = CmdWindowPos;

        ICmdUI ui = obj.GetComponent <ICmdUI>();
        foreach (string cmd in cmdList)
        {
            ui.CmdAdd(cmd);
        }

        printedCmdWindow = obj;
    }

    public void PrintPlayerInformation(IPawn pawn)
    {
        DestroyPlayerInformation();

        var obj = Instantiate(PawnInfo);
        obj.transform.SetParent(transform, false);

        RectTransform rect = obj.GetComponent<RectTransform>();
        rect.anchoredPosition = PawnInfoPos;

        IPawnInfoUI ui = obj.GetComponent<IPawnInfoUI>();
        ui.Name = pawn.Name;
        ui.SetHP(pawn.MaxHP, pawn.HP);

        printedPawnInfo = obj;
    }

    private void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
