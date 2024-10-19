﻿using System.Collections;
using UnityEngine;

public class RangeMover : MonoBehaviour, RangeMovable
{
    private float range;

    [SerializeField]
    private float moveSpeed = 0.15f;

    private Vector3 movedir;

    private Vector3 firstPos;

    public float Range { set => range = value; }

    public void SetMoveDir(Vector3 dir)
    {
        movedir = dir;
    }

    public void SetFirstPos()
    {
        firstPos = transform.position;
        movedir = Vector3.zero;
    }

    // Use this for initialization
    void Start()
    {
        SetFirstPos();
    }

    // Update is called once per frame
    void Update()
    {
        float dt = Time.deltaTime;
        transform.position += movedir * moveSpeed*dt;
        Debug.Log(moveSpeed);
        if (Vector2.Distance(transform.position, firstPos) > range)
        {
            transform.position = firstPos + (transform.position - firstPos).normalized * range;
        }

        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }
}