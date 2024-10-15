using System.Collections;
using UnityEngine;

public class RangeAttackCommand : IRangeAttackCommand
{
    public ActionCmdType Type => ActionCmdType.RangeAttack;

    private string name;

    public string Name => throw new System.NotImplementedException();

    public float Range => throw new System.NotImplementedException();

    public float Damage => throw new System.NotImplementedException();

    public T GetMySelf<T>()
    {
        if (typeof(T) is IRangeAttackCommand) return (T)(object)this;
        else return default(T);
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