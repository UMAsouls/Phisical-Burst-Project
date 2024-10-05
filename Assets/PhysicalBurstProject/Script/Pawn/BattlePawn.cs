using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlePawn : MonoBehaviour, 
    IPawn, IDGettable, ICmdSelectablePawn, PawnOptionSettable, ActablePawn, SpeedGettable, ActionSettable, BefActPawn
{

    protected IStatus status;

    private int id;

    private int mana;

    private Vector2 virtualPos;

    private int actPoint;

    public float attack => status.Attack;

    public float defence => status.Defence;

    public float speed => status.Speed;

    public float range => status.Range;

    public bool death => false;

    public string Name => status.Name;

    public int MaxHP => status.MaxHP;

    public int HP => status.HP;

    public int ID => id;

    public IStatus Status { set => status = value; }

    public Vector2 Position => transform.position;

    public int Mana => mana;

    public Vector2 VirtualPos => virtualPos;

    public int ActionNum => throw new System.NotImplementedException();

    int PawnOptionSettable.ID { set => id = value; }
    Vector2 BefActPawn.VirtualPos { set => virtualPos = value; }

    public void Action()
    {
        throw new System.NotImplementedException();
    }

    public void ActionAdd(IAction action)
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

    public bool useActPoint(int point)
    {
        if(actPoint < point) return false;

        actPoint -= point;
        return true;
    }


    // Start is called before the first frame update
    void Start()
    {
        mana = 0;
        virtualPos = transform.position;
        actPoint = 2;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
