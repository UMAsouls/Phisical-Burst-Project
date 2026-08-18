using ModestTree;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class BattleCmdSlotUIPrinter : UIPrinter, IBattleCmdSlotUIPrinter
{
    [SerializeField]
    GameObject ui;

    [SerializeField]
    Vector2 pos;

    [SerializeField]
    Vector2 LeftPos;

    [SerializeField]
    Vector2 RightPos;

    private Stack<GameObject> printedUI;

    public void DestroyUI()
    {
        if(printedUI.Count > 0)
        {
            var obj = printedUI.Pop();
            Destroy(obj);
        }
    }

    public SlotWindowControlable PrintUI()
    {
        return PrintUIAt(this.pos);
    }

    public SlotWindowControlable PrintUIAt(Vector2 pos)
    {
        var obj = PrintUIAsChildAt(ui, pos);

        printedUI.Push(obj);
        return obj.GetComponent<SlotWindowControlable>();
    }

    public SlotWindowControlable PrintUIInBattle(Vector2 worldPoint, Vector2 dir)
    {
        Vector2 pos = WorldToUIPoint(worldPoint);

        pos += (dir.normalized) * 500;

        int x = 220;
        int y = 150;

        if(pos.x + 2*x  > WINDOW_WIDTH) pos.x = WINDOW_WIDTH - 2*x;
        if(pos.x < 0) pos.x = 0;

        if (pos.y + 2*y + 200 > WINDOW_HEIGHT) pos.y = WINDOW_HEIGHT - 2*y - 200;
        if (pos.y < - WINDOW_HEIGHT) pos.y = 0;

        return PrintUIAt(pos);
    }

    public SlotWindowControlable PrintUILeft()
    {
        return PrintUIAt(LeftPos);
    }

    public SlotWindowControlable PrintUIRight()
    {
        return PrintUIAt(RightPos);
    }

    private void Awake()
    {
        printedUI = new Stack<GameObject>();
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