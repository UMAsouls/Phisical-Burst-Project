using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class CmdWindowSetter : MonoBehaviour, ICmdUI,ICmdTextRectGetter
{
    private List<RectTransform> cmdTextRects = new List<RectTransform>();

    [SerializeField]
    private GameObject CmdTextUI;

    private int cmdNum = 0;

    private float maxWidth = 0;

    [SerializeField]
    private int fontSize;

    [SerializeField]
    private float textSpace;

    [SerializeField]
    private Vector2 firstCmdPos;

    private IWindowScaler scaler;

    private ISelectorController selectorController;

    private int selectorIndex = 0;

    public List<RectTransform> CmdTextRects => cmdTextRects;

    public void CmdAdd(string text)
    {
        var obj = Instantiate(CmdTextUI);
        obj.transform.SetParent(transform, false);

        var rect = obj.GetComponent<RectTransform>();
        var height = Vector2.down * (fontSize+textSpace) * cmdNum;
        rect.localPosition = (Vector3)(firstCmdPos + height);

        ICmdTextSetable textSetter = obj.GetComponent<ICmdTextSetable>();
        textSetter.FontSize = fontSize;
        textSetter.Text = text;
        var width = textSetter.TextWidth;

        if(maxWidth < width) maxWidth = width;

        cmdTextRects.Add(rect);
        cmdNum++;

        ScaleUpdate();
    }

    private void ScaleUpdate()
    {
        Vector2 firstSize = new Vector2(2*firstCmdPos.x, -2*firstCmdPos.y);
        scaler.Scale = new Vector2(maxWidth+fontSize, (fontSize+textSpace) * cmdNum - textSpace) + firstSize;
    }

    

    private void Awake()
    {
        cmdNum = 0;
        scaler = GetComponentInChildren<IWindowScaler>();
        selectorController = GetComponentInChildren<ISelectorController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        selectorController.Scale = fontSize;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
