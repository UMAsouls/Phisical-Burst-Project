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
        rectTransform.sizeDelta = new Vector2(FontSize, FontSize * text.Length);
    }

    public string Text { set => SetCommandText(value); }
    public float FontSize { get => tmp.fontSize; set => tmp.fontSize = value; }

    // Start is called before the first frame update
    void Start()
    {
        tmp = GetComponent<TextMeshProUGUI>();
        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
