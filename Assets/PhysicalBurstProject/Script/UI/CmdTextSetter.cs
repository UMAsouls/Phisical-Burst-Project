using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
[RequireComponent(typeof(RectTransform))]
public class CmdTextSetter : MonoBehaviour, ICmdTextSetable
{

    private TextMeshProUGUI tmp;
    private RectTransform rectTransform;

    void SetCommandText(string text)
    {
        tmp.text = text;
        rectTransform.sizeDelta = new Vector2(TextWidth, FontSize);
    }

    void fontSizeUpdate(float size)
    {
        tmp.fontSize = size;
        rectTransform.sizeDelta = new Vector2(TextWidth, size);
    }

    public string Text { set => SetCommandText(value); }
    public float FontSize { get => tmp.fontSize; set => fontSizeUpdate(value); }
    public float TextWidth { get => tmp.preferredWidth; }

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
