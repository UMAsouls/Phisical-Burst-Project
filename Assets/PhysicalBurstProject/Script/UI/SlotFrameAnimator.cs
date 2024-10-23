using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SlotFrameAnimator : MonoBehaviour
{


    private Material mat;

    public void SetBurst(bool v)
    {
        if (v) mat.SetInt("_Burst", 1);
        else mat.SetInt("_Burst", 0);
    }

    public void SetFocus(bool v)
    {
        if (v) mat.SetInt("_Selected", 1);
        else mat.SetInt("_Selected", 0);
    }

    public void TurnBlue(float v)
    {
        mat.SetFloat("_Blue", v);
    }

    public void TurnYellow(float v)
    {
        mat.SetFloat("_Yellow", v);
    }

    // Use this for initialization
    void Start()
    {
        mat = GetComponent<Image>().material;
    }

    // Update is called once per frame
    void Update()
    {

    }
}