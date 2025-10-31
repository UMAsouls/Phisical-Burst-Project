using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AutoCmdUIController : MonoBehaviour, ICmdUI, ICmdSelectorController
{
    [SerializeField]
    GameObject autoCmdText;

    [SerializeField]
    GameObject window;

    List<AutoCmdText> cmdTexts = new List<AutoCmdText>();

    private int idx = 0;

    public void CmdAdd(string text)
    {
        var obj = Instantiate(autoCmdText);
        obj.transform.SetParent(window.transform, false);

        var cmdText = obj.GetComponent<AutoCmdText>();
        cmdText.SetText(text);

        cmdTexts.Add(cmdText);
    }

    public void Move(int dir)
    {
        if (cmdTexts.Count <= 0) return;
        cmdTexts[idx].UnSelect();
        idx = (idx + dir)%cmdTexts.Count;
        if (idx < 0) idx = cmdTexts.Count - 1; 
        cmdTexts[idx].Select();
    }

    public void Set(int pos)
    {
        if (cmdTexts.Count <= 0) return;
        cmdTexts[idx].UnSelect();
        idx = pos % cmdTexts.Count;
        if (idx < 0) idx = 0;
        cmdTexts[idx].Select();
    }

    public void reset()
    {
        foreach (AutoCmdText cmdText in cmdTexts)
        {
            Destroy(cmdText.gameObject);
        }
        cmdTexts.Clear();
        idx = 0;
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