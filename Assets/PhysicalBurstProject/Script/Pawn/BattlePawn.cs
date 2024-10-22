using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(SelectablePawn))]
[RequireComponent(typeof(IPawnAnimator))]
public abstract class BattlePawn : MonoBehaviour, 
    IPawn, IDGettable, PawnOptionSettable, ActablePawn, ActionSelectable, ActionSettable,
    CommandActionSettable, IVirtualPawn, BattleCmdSelectable, PawnTypeGettable, SelectedPawn, AttackAble, PawnActInterface
{

    protected IStatus status;

    protected IVirtualPawn virtualPawn;

    private SelectablePawn selectable;

    private IPawnAnimator animator;

    private MoveActionUnit moveUnit;

    [SerializeField]
    private GameObject virtualObjBase;
    private GameObject virtualObj;

    private int id;

    private int mana = 0;

    private Vector2 virtualPos;

    private int actPoint;

    private int actMax = 2;

    private IActionCommand[] actCmds;

    private IBattleCommand[] battleCmds;

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

    public IBattleCommand[] BattleCommands { get => battleCmds; set => battleCmds = value; }

    public float AttackRange => status.AttackRange;

    public abstract PawnType Type { get; }

    public bool IsMove => throw new System.NotImplementedException();

    public virtual void ActionAdd(IAction action)
    {
       actions.Add(action);
    }

    public ICommand[] GetCommands()
    {
        throw new System.NotImplementedException();
    }

    public virtual IActionCommand[] GetActionCommands()
    {
        return actCmds;
    }

    public virtual async UniTask MovePos(Vector2 delta)
    {
        await moveUnit.Move(delta, this);
    }

    public virtual async UniTask TurnStart()
    {
        virtualObj = Instantiate(virtualObjBase, transform.position, Quaternion.identity);
        virtualPawn = virtualObj.GetComponent<IVirtualPawn>();
        virtualPawn.VirtualPos = transform.position;
        actPoint = actMax;
    }

    public virtual async UniTask TurnEnd()
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
        var cAct = actions.Last();
        cAct.CancelAct(this);

        actions.Remove(cAct);

        return true;
    }
    public string[] GetActionNames()
    {
        string[] names = new string[actions.Count];
        for(int i = 0; i < names.Length; i++) names[i] = actions[i].GetActionName();
        return names;
    }

    public void SelectedFocus()
    {
        selectable.OnFocus();
    }

    public void SelectedUnFocus()
    {
        selectable.OnUnFocus();
    }

    public void Attack(int attack)
    {
        throw new System.NotImplementedException();
    }

    public UniTask Battle(IBattleCommand[] cmds, AttackAble pawn)
    {
        throw new System.NotImplementedException();
    }

    public UniTask Action(IActionCommandBehaviour action)
    {
        throw new System.NotImplementedException();
    }

    public UniTask EmergencyBattle()
    {
        throw new System.NotImplementedException();
    }



    // Start is called before the first frame update
    protected virtual void Start()
    {
        moveUnit = GetComponent<MoveActionUnit>();
        animator = GetComponent<IPawnAnimator>();
        selectable = GetComponent<SelectablePawn>();
        mana = 0;
        virtualPos = transform.position;
        actMax = 2;
        actPoint = actMax;
        actions = new List<IAction>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }

    public void MoveAnimation(Vector2 dir) => animator.MoveAnimation(dir);

    public void EndMove() => animator.EndMove();

    public async UniTask DoAction()
    {
        foreach (var action in actions) await action.DoAct(this);
    }
}
