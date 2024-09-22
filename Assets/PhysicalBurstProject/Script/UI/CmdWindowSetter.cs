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
        obj.transform.SetParent(transform, false);

        var rect = obj.GetComponent<RectTransform>();
        var height = Vector2.down * fontSize * cmdNum;
        rect.localPosition = (Vector3)(firstCmdPos + height);

        ICmdTextSetable textSetter = obj.GetComponent<ICmdTextSetable>();
        textSetter.FontSize = fontSize;
        textSetter.Text = text;
        var width = textSetter.TextWidth;

        if(maxWidth < width) maxWidth = width;

        cmdTexts.Add(text);
        cmdNum++;

        ScaleUpdate();
    }

    private void ScaleUpdate()
    {
        Vector2 firstSize = new Vector2(2*firstCmdPos.x, -2*firstCmdPos.y);
        scaler.Scale = new Vector2(maxWidth, fontSize * cmdNum) + firstSize;
    }

    private void Awake()
    {
        cmdNum = 0;
        scaler = GetComponentInChildren<IWindowScaler>();
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
