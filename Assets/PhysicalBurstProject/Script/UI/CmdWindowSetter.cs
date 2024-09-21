using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class CmdWindowSetter : MonoBehaviour, ICmdUI
{
    private List<string> cmdTexts = new List<string>();

    [SerializeField]
    private GameObject CmdTextUI;

    private int cmdNum = 0;

    private float maxWidth = 0;

    [SerializeField]
    private int fontSize;

    [SerializeField]
    private Vector2 firstCmdPos;

    private IWindowScaler scaler;

    public void CmdAdd(string text)
    {
        var obj = Instantiate(CmdTextUI);
        transform.SetParent(obj.transform, false);

        var rect = obj.GetComponent<RectTransform>();
        var height = Vector2.down * fontSize * cmdNum;
        rect.localPosition = (Vector3)(firstCmdPos + height);

        ICmdTextSetable textSetter = obj.GetComponent<ICmdTextSetable>();
        textSetter.Text = text;
        textSetter.FontSize = fontSize;

        if(maxWidth < fontSize*text.Length) maxWidth = fontSize * text.Length;

        cmdTexts.Add(text);
        cmdNum++;

        ScaleUpdate();
    }

    private void ScaleUpdate()
    {
        scaler.Scale = new Vector2(maxWidth, fontSize * cmdNum) + firstCmdPos;
    }

    // Start is called before the first frame update
    void Start()
    {
        cmdNum = 0;
        scaler = GetComponentInChildren<IWindowScaler>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
