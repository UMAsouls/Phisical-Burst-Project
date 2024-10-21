using System.Collections;
using UnityEngine;

public class SelectPhazeCamera : RangeMover, SelectPhazeCameraControllable
{
    public Vector2 Position { get => transform.position; set => SetPosition(value); }
    public bool RangeMode { get => rangeMode; set => rangeMode = value; }

    private bool rangeMode;

    public void SetFirstPos(Vector2 pos)
    {
        transform.position = pos;
        SetFirstPos();
    }

    private void SetPosition(Vector2 pos)
    {
        Vector3 p = pos;
        p.z = -1;
        transform.position = p;
    }

    // Use this for initialization
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        if(rangeMode)
        {
            base.Update();
            var pos = transform.position;
            pos.z = -1;
            transform.position = pos;
        }
    }
}