using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BattleUIPrinter : MonoBehaviour,IBattleUIPrinter 
{
    [Inject]
    BattleUIPrinterSetter setter;

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
        obj.transform.localPosition = CmdWindowPos;

        ICmdUI ui = obj.GetComponent<ICmdUI>();
        ui.CmdAdd("èPåÇ");
        ui.CmdAdd("ë“ÇøïöÇπ");
        ui.CmdAdd("à⁄ìÆ");
        ui.CmdAdd("çsìÆ");
    }

    public void PrintPlayerInformation(IPawn pawn)
    {
        var obj = Instantiate(PawnInfo);
        obj.transform.SetParent(transform, false);
        obj.transform.localPosition = PawnInfoPos;

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
