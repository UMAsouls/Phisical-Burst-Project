using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlePawn : MonoBehaviour, 
    IPawn, IDGettable, ICmdSelectablePawn, PawnOptionSettable,ActablePawn,SpeedGettable
{

    protected IStatus status;

    private int id;

    private int mana;

    public float attack 
    {
        get { return status.Attack; }
    }

    public float defence
    {
        get { return status.Defence; }
    }

    public float speed
    {
        get { return status.Speed; }
    }

    public float range
    {
        get { return status.Range; }
    }

    public bool death
    {
        get { return false; }
    }

    public string Name => status.Name;

    public int MaxHP => status.MaxHP;

    public int HP => status.HP;

    public int ID => id;

    public IStatus Status { set => status = value; }

    public Vector2 Position => transform.position;

    public int Mana => mana;

    int PawnOptionSettable.ID { set => id = value; }

    public void Action()
    {
        throw new System.NotImplementedException();
    }

    public ICommand[] GetCommands()
    {
        throw new System.NotImplementedException();
    }

    public async UniTask movePos(Vector2 delta)
    {
        await transform.DOMove((Vector3)delta, 0.5f).AsyncWaitForCompletion();
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
