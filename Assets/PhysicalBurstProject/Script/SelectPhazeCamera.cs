using System.Collections;
using UnityEngine;

public class SelectPhazeCamera : MonoBehaviour, SelectPhazeCameraControllable
{
    public Vector2 Position { get => transform.position; set => SetPosition(value); }

    private void SetPosition(Vector2 pos)
    {
        Vector3 p = pos;
        p.z = -1;
        transform.position = p;
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