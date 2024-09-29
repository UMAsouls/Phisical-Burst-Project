using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BattleUIPrinter : MonoBehaviour,IBattleUIPrinter 
{
    [Inject]
    BattleUIPrinterSetter setter;

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

    public void PrintActionSelecter()
    {
        var obj = Instantiate(CmdWindow);
        obj.transform.SetParent(transform, false);

        RectTransform rect = obj.GetComponent<RectTransform>();
        rect.anchoredPosition = CmdWindowPos;

        ICmdUI ui = obj.GetComponent<ICmdUI>();
        ui.CmdAdd("�P��");
        ui.CmdAdd("�҂�����");
        ui.CmdAdd("�ړ�");
        ui.CmdAdd("�s��");
    }

    public void PrintCmdSelecter(string[] cmdList)
    {

        var obj = diContainer.InstantiatePrefab(CmdWindow);
        obj.transform.SetParent(transform, false);

        RectTransform rect = obj.GetComponent<RectTransform>();
        rect.anchoredPosition = CmdWindowPos;

        ICmdUI ui = obj.GetComponent <ICmdUI>();
        foreach (string cmd in cmdList)
        {
            ui.CmdAdd(cmd);
        }
    }

    public void PrintPlayerInformation(IPawn pawn)
    {
        var obj = Instantiate(PawnInfo);
        obj.transform.SetParent(transform, false);

        RectTransform rect = obj.GetComponent<RectTransform>();
        rect.anchoredPosition = PawnInfoPos;

        IPawnInfoUI ui = obj.GetComponent<IPawnInfoUI>();
        ui.Name = pawn.Name;
        ui.SetHP(pawn.MaxHP, pawn.HP);
    }

    private void Awake()
    {
        setter.BattleUIPrinter = this;
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
