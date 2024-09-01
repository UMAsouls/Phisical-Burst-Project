using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlePorn : MonoBehaviour, IPorn
{

    protected IStatus status;

    protected IAccessorie[] accessories;


    public float attack 
    {
        get { return 0; }
    }

    public float defence
    {
        get { return 0; }
    }

    public float speed
    {
        get { return 0; }
    }

    public float range
    {
        get { return 0; }
    }

    public bool death
    {
        get { return false; }
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
