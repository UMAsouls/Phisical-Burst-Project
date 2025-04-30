using System.Collections;
using UnityEngine;

public class DamageHealNum : MonoBehaviour
{
    [SerializeField]
    private int deleteTIme;

    [SerializeField]
    private float moveSpeed;

    private float time;
    // Use this for initialization
    void Start()
    {
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        var dt = Time.deltaTime;
        time += dt;
        transform.position += new Vector3(0, moveSpeed, 0)*dt;
        if(time > deleteTIme) Destroy(gameObject);
    }
}