using System.Collections;
using UnityEngine;

public class RangeCircleScaler : MonoBehaviour, IRangeCircleScaler
{
    public void SetRadius(float scale)
    {
        transform.localScale = new Vector3(scale*2, scale*2, 1);
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}