using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SlotFrameAnimator : MonoBehaviour
{

    [SerializeField]
    private bool Burst;


    private Material mat;

    private void SetBurst(bool v)
    {
        if (v) mat.SetInt("_Burst", 1);
        else mat.SetInt("_Burst", 0);
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