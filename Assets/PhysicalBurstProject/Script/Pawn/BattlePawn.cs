using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlePawn : MonoBehaviour, IPawn, IDGettable, ICmdSelectable
{

    protected IStatus status;

    protected IAccessorie[] accessories;

    private int id;


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

    public string Name => "";

    public int MaxHP => 100;

    public int HP => 50;

    public int ID => id;

    public ICommand[] GetCommands()
    {
        throw new System.NotImplementedException();
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
