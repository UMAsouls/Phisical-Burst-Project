using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class HPbar : MonoBehaviour, GageSetable
{
    public void Set(int maxValue, int value)
    {
        m_Slider.value = value / maxValue;
    }

    private Slider m_Slider;

    // Start is called before the first frame update
    void Start()
    {
        m_Slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
