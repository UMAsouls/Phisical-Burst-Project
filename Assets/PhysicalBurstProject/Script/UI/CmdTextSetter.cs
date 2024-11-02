using Cysharp.Threading.Tasks;
using PlasticGui.WorkspaceWindow.Locks;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
[RequireComponent(typeof(RectTransform))]
public class CmdTextSetter : MonoBehaviour, ICmdTextSetable
{
    [SerializeField]
    private TextMeshProUGUI tmp;
    [SerializeField]
    private RectTransform rectTransform;

    void SetCommandText(string text)
    {
        tmp.text = text;
        rectTransform.sizeDelta = new Vector2(TextWidth, FontSize);
    }

    void fontSizeUpdate(float size)
    {
        tmp.fontSize = size;
        rectTransform.sizeDelta = new Vector2(TextWidth, size+TextSpace);
    }

    void SetTextSpace(float value)
    {
        textSpace = value;
        rectTransform.sizeDelta = new Vector2(TextWidth, FontSize + TextSpace);
    }

    public string Text { get => tmp.text; set => SetCommandText(value); }
    public float FontSize { get => tmp.fontSize; set => fontSizeUpdate(value); }
    public float TextWidth { get => tmp.preferredWidth; }

    private float textSpace = 0;
    public float TextSpace { get => textSpace; set => SetTextSpace(value); }

    private void Awake()
    {
        tmp = GetComponent<TextMeshProUGUI>();
        rectTransform = GetComponent<RectTransform>();
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
