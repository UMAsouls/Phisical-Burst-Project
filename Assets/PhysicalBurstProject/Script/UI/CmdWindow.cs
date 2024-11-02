using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
[ExecuteAlways]
public class CmdWindow : MonoBehaviour, ICmdUI, ICmdTextRectGetter
{
    [SerializeField]
    private List<GameObject> texts = new List<GameObject>();

    private List<RectTransform> cmdTextRects;

    public List<RectTransform> CmdTextRects => cmdTextRects;

    [SerializeField]
    private GameObject selector;

    [SerializeField]
    private float fontSize;

    [SerializeField]
    private float textspace;

    [SerializeField]
    private GameObject CmdText;

    private float maxWidth;

    private float height;

    private RectTransform rect;

    public void CmdAdd(string text)
    {
        throw new System.NotImplementedException();
    }

    private void SetCmdTextRects()
    {
        cmdTextRects = new List<RectTransform>();
        foreach(var obj in texts)
        {
            cmdTextRects.Add(obj.GetComponent<RectTransform>());
        }
    }

    private void SizeUpdate()
    {
        maxWidth = 0;
        height = 0;
        foreach (var obj in texts)
        {
            ICmdTextSetable cmdText = obj.GetComponent<ICmdTextSetable>();
            cmdText.FontSize = fontSize;
            cmdText.TextSpace = textspace;

            var width  = cmdText.TextWidth;
            if (width > maxWidth) maxWidth = width;

            var rectTransfrom = obj.GetComponent<RectTransform>();
            rectTransfrom.anchoredPosition = Vector3.down * height;
            Debug.Log(rectTransfrom.anchoredPosition);
            height += rectTransfrom.sizeDelta.y;
        }

        ScaleUpdate();
    }

    private void SelectorUpdate()
    {
        ISelectorController controller = selector.GetComponent<ISelectorController>();
        controller.Scale = fontSize;
    }

    private void SetMaxWidth()
    {
        
    }

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        SetCmdTextRects();
        SizeUpdate();
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        SelectorUpdate();
        SetCmdTextRects();
        SizeUpdate();
        Debug.Log(rect.sizeDelta);
    }
#endif

    private void ScaleUpdate()
    { 
        rect.sizeDelta = new Vector2(maxWidth + fontSize, height);
    }

    // Use this for initialization
    void Start()
    {
        SizeUpdate();
        SelectorUpdate();
    }

    // Update is called once per frame
    void Update()
    {

    }
}