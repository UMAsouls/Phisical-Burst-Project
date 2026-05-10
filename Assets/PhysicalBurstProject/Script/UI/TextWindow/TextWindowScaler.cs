using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
[ExecuteAlways]
public class TextWindowScaler : MonoBehaviour, IWindowScaler
{
    public Vector2 Scale
    {
        set
        {
            //rectTransform‚Ìwidth, height‚ÍsizeDelta‚Å
            rectTransform.sizeDelta = value;
        }
    }
        
    private RectTransform rectTransform;

    private void Awake()
    {
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
