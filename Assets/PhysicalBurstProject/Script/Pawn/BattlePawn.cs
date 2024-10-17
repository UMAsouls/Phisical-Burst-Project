using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BattlePawn : MonoBehaviour, 
    IPawn, IDGettable, ICmdSelectablePawn, PawnOptionSettable, ActablePawn, ActionSelectable, ActionSettable,
    CommandActionSettable, IVirtualPawn
{

    protected IStatus status;

    protected IVirtualPawn virtualPawn;

    [SerializeField]
    private GameObject virtualObjBase;
    private GameObject virtualObj;

    private int id;

    private int mana;

    private Vector2 virtualPos;

    private int actPoint;

    private int actMax;

    private IActionCommand[] actCmds;

    private List<IAction> actions;

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

    public Vector2 VirtualPos { get => virtualPawn.VirtualPos; set => virtualPawn.VirtualPos = value; }

    public int ActPoint => actPoint;

    int PawnOptionSettable.ID { set => id = value; }

    public IActionCommand[] ActionCommands { get => actCmds; set => actCmds = value; }
    public float VirtualRange { get => virtualPawn.VirtualRange; set => virtualPawn.VirtualRange = value; }
    public float VirtualMana { get => virtualPawn.VirtualMana; set => virtualPawn.VirtualMana = value; }
    public float VirtualHP { get => virtualPawn.VirtualHP; set => virtualPawn.VirtualHP = value; }
    public bool IsBurst { get => virtualPawn.IsBurst; set => virtualPawn.IsBurst = value; }

    public async void Action()
    {
        foreach (var action in actions)
        {
            await action.DoAct(this);
        }
    }

    public void ActionAdd(IAction action)
    {
       actions.Add(action);
    }

    public ICommand[] GetCommands()
    {
        throw new System.NotImplementedException();
    }

    public IActionCommand[] GetActionCommands()
    {
        return actCmds;
    }

    public async UniTask movePos(Vector2 delta)
    {
        await transform.DOMove((Vector3)delta, 0.5f);
    }

    public async UniTask TurnStart()
    {
        virtualObj = Instantiate(virtualObjBase, transform.position, Quaternion.identity);
        virtualPawn = virtualObj.GetComponent<IVirtualPawn>();
        virtualPawn.VirtualPos = transform.position;
        actPoint = actMax;
    }

    public async UniTask TurnEnd()
    {
        actions = new List<IAction>();
        VirtualPos = transform.position;
        virtualPawn = null;
        Destroy(virtualObj);
    }

    public bool UseActPoint(int point)
    {
        if(actPoint < point) return false;

        actPoint = Mathf.Clamp(actPoint - point, 0, actMax);
        return true;
    }

    public bool CancelSelect()
    {

        if(actions.Count == 0) return false;

        var cAct = actions.Last<IAction>();
        Debug.Log("bef: " + virtualPos.ToString());
        cAct.CancelAct(this);
        Debug.Log("aft: " + virtualPos.ToString());

        actions.Remove(cAct);

        return true;
    }


    // Start is called before the first frame update
    void Start()
    {
        mana = 0;
        virtualPos = transform.position;
        actMax = 2;
        actPoint = actMax;
        actions = new List<IAction>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
