using DG.Tweening.Plugins.Options;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class SelectorController : MonoBehaviour, ISelectorController
{
    private float spaceSize;

    public float Scale { set => rectTransform.sizeDelta = new Vector2(value, value); }

    public void Move(Vector2 pos)
    {
        rectTransform.localPosition = pos;
    }

    RectTransform rectTransform;

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
