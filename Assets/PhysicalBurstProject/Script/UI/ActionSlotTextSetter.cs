using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using UnityEngine;


[RequireComponent(typeof(RectTransform))]
public class ActionSlotTextSetter : MonoBehaviour, IActionTextSettable
{ 
    private TextMeshProUGUI tmp;
    private RectTransform rectTransform;

    void SetActionText(string text)
    {
        tmp.text = text;
        SizeUpdate();
    }

    void fontSizeUpdate(float size)
    {
        tmp.fontSize = size;
        SizeUpdate();
    }

    public void SizeUpdate()
    {
        float size = tmp.fontSize;
        float width = TextWidth;
        rectTransform.sizeDelta = new Vector2(width, size);
    }

    public string Text { set => SetActionText(value); }
    public float FontSize { get => tmp.fontSize; set => fontSizeUpdate(value); }
    public float TextWidth { get => tmp.preferredWidth + 20; }

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
