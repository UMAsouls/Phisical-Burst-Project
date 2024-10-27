using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(SelectablePawn))]
[RequireComponent(typeof(IPawnAnimator))]
[RequireComponent(typeof(EffectUnit))]
public abstract class BattlePawn : MonoBehaviour, 
    IPawn, IDGettable, PawnOptionSettable, ActablePawn, ActionSelectable, ActionSettable,
    CommandActionSettable, IVirtualPawn, BattleCmdSelectable, PawnTypeGettable, SelectedPawn, AttackAble, PawnActInterface
{

    protected IStatus status;

    protected IVirtualPawn virtualPawn;

    private SelectablePawn selectable;

    private IPawnAnimator animator;

    [Inject]
    private MoveActionUnit moveUnit;

    private EffectUnit effectUnit;

    [Inject]
    BattleActionUnit battleActionUnit;

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

    protected IBattleCommand[] emergencyCmds;

    private List<IAction> actions;

    private CancellationToken token;

    [Inject]
    IStandardUIPritner standardUIPritner;

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

    public Vector2 Position { get => transform.position; set => transform.position = value; }

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

    public IBattleCommand[] EmergencyCmds => emergencyCmds;

    public int Priority { get => status.Priority; set => status.Priority = value; }

    public bool Burst { get; set; }

    public bool Avoid { get; set; }

    public float Guard { get; set; }

    public bool DamageAble { get; set; } = false;

    public bool IsStun { get; set; } = false;
    public bool AttackEnd { get; set; } = false;

    public virtual void ActionAdd(IAction action)
    {
       actions.Add(action);
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
        IsStun = false;
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

    public async UniTask Battle(IBattleCommand[] cmds, AttackAble target)
    {
        await battleActionUnit.Battle(cmds, target, this);
    }

    public UniTask Action(IActionCommandBehaviour action)
    {
        throw new System.NotImplementedException();
    }

    public abstract UniTask EmergencyBattle();



    // Start is called before the first frame update
    protected virtual void Start()
    {
        animator = GetComponent<IPawnAnimator>();
        selectable = GetComponent<SelectablePawn>();
        effectUnit = GetComponent<EffectUnit>();
        mana = 0;
        virtualPos = transform.position;
        actMax = 2;
        actPoint = actMax;
        actions = new List<IAction>();
        token = this.GetCancellationTokenOnDestroy();
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

    public void FightStart()
    {
        Avoid = false;
        Guard = 0;
        DamageAble = false;
        AttackEnd = false;
    }

    public void FightEnd()
    {
        DamageAble = true;
        Avoid = false;
        Guard = 0;
        AttackEnd = true;
    }

    public void PhysicalBurst()
    {
        status.Burst();
        IsBurst = true;
        effectUnit.Burst();
        animator.ChangeBurst();
    }

    public void Stun()
    {
        IsStun = true;
        effectUnit.Stun();
        animator.ChangeStun();
    }

    public async UniTask<bool> Damage(float damage)
    {
        await UniTask.WaitUntil(() => DamageAble, cancellationToken: token);
        if(Avoid) return false;
        int d =status.Damage(damage*(1 - Guard));
        effectUnit.Damage(d);
        return true;
    }
}
