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
    IPawnInfo, IDGettable, PawnOptionSettable,  
    IVirtualPawn, PawnTypeGettable, SelectedPawn,
    AttackAble, PawnActInterface, PosGetPawn, AmbushPawn, IBattlePawn
{
    [Inject]
    protected IPawnActionManager pawnActionManager;

    protected IStatus status;

    protected IVirtualPawn virtualPawn;

    private SelectablePawn selectable;

    private IPawnAnimator animator;

    [Inject]
    private MoveActionUnit moveUnit;

    private EffectUnit effectUnit;

    [Inject]
    BattleActionUnit battleActionUnit;

    [Inject]
    AmbushUnit ambushUnit;

    [SerializeField]
    private GameObject virtualObjBase;
    private GameObject virtualObj;

    private int id;

    private int mana = 0;

    private Vector2 virtualPos;

    private int actPoint;

    private int actMax = 2;

    private bool death = false;

    private IActionCommand[] actCmds;

    private IBattleCommand[] battleCmds;

    protected IBattleCommand[] emergencyCmds;

    private List<IAction> actions;

    private CancellationToken token;

    private CancellationTokenSource ambushTokenSource;

    [Inject]
    IStandardUIPritner standardUIPritner;

    [Inject]
    SystemSEPlayable sePlayer;

    [Inject]
    MiniStatusPrinter miniStatusPrinter;

    [Inject]
    protected IPawnGettable strage;

    [Inject]
    protected IObservable<TurnPhaseFrag> turnPhaseObservable;

    public float attack => status.Attack;

    public float defence => status.Defence;

    public float speed => status.Speed;

    public float range => status.Range;

    public bool Death => death;

    public string Name => status.Name;

    public int MaxHP => status.MaxHP;

    public int HP => status.HP;

    public int ID => id;

    public IStatus Status { set => status = value; }

    public Vector2 Position { get => transform.position; set => transform.position = value; }

    public float Size => selectable.Size;

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

    public bool IsMove { get; set; }

    public IBattleCommand[] EmergencyCmds => emergencyCmds;

    public int Priority { get => status.Priority; set => status.Priority = value; }

    public bool Burst { get; set; }

    public bool Avoid { get; set; }

    public float Guard { get; set; }

    public bool DamageAble { get; set; } = false;

    public bool IsStun { get; set; } = false;
    public bool AttackEnd { get; set; } = false;

    public bool GetAmbushed { get; set; }

    public bool ActionStop { get; set; }

    public IPawnActionManager ActionManager => pawnActionManager;

    IStatus IBattlePawn.Status => status;

    public IVirtualPawn VirtualPawn => virtualPawn;

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
        actPoint = actMax;
    }

    protected void VirtualPawnSet()
    {
        virtualObj = Instantiate(virtualObjBase, transform.position, Quaternion.identity);
        virtualPawn = virtualObj.GetComponent<IVirtualPawn>();
        Debug.Log($"vp: {virtualPawn}");
        virtualPawn.VirtualPos = transform.position;
        virtualPawn.VirtualMana = mana;
        virtualPawn.VirtualHP = HP;
        virtualPawn.VirtualRange = range;
    }

    public void SelectStart()
    {
        VirtualPawnSet();

        ambushTokenSource?.Cancel();
    }

    public void SelectEnd()
    {
        virtualPawn = null;
        Destroy(virtualObj);
    }

    public virtual async UniTask TurnEnd()
    {
        actions = new List<IAction>();
        IsStun = false;
        status.Priority = 3;
        ClearBurst();
        ClearStun();
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
        cAct.CancelAct(ActionManager, VirtualPawn, status);

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

    public async UniTask AmbushEffect() => await effectUnit.Ambush();

    public async UniTask Battle(IBattleCommand[] cmds, AttackAble target)
    {
        await battleActionUnit.Battle(cmds, target);
    }

    public async UniTask Action(IActionCommandBehaviour action)
    {
        await action.DoAction(ID);
        UseMana((int)action.UseMana);
    }

    public async UniTask Ambush(float range)
    {
        ambushTokenSource = new CancellationTokenSource();
        ambushUnit.Ambush(this, range).Forget();
    }

    public abstract UniTask EmergencyBattle(AttackAble target);

    public void AttackEmote(Vector2 dir) => animator.AttackEmote(dir);

    public void DodgeEmote(Vector2 dis) => animator.DodgeEmote(dis);

    // Start is called before the first frame update
    protected virtual void Start()
    {
        animator = GetComponent<IPawnAnimator>();
        status.Subscribe(animator);
        selectable = GetComponent<SelectablePawn>();
        effectUnit = GetComponent<EffectUnit>();
        status.Subscribe(effectUnit);
        status.Subscribe(sePlayer);
        mana = 0;
        virtualPos = transform.position;
        actMax = 2;
        actPoint = actMax;
        actions = new List<IAction>();
        token = this.GetCancellationTokenOnDestroy();
        pawnActionManager.init(id, Type);
        FightEnd();

        turnPhaseObservable.Subscribe(status);
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }

    private void OnDestroy()
    {
        ambushTokenSource?.Cancel();
    }

    public void MoveAnimation(Vector2 dir) => animator.MoveAnimation(dir);

    public void EndMove() => animator.EndMove();

    public async UniTask DoAction()
    {
        ActionStop = false;
        foreach (var action in actions)
        {
            Debug.Log($"{status.Name}: {action.GetActionName()}");
            if(!ActionStop) await action.DoAct(this);
        }
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
        Burst = true;
        effectUnit.Burst();
        animator.ChangeBurst();
        sePlayer.BurstSE();
    }

    public void ClearBurst()
    {
        Burst = false;
        animator.ChangeNormal();
    }

    public void Stun()
    {
        if (Death) return;
        IsStun = true;
        effectUnit.Stun();
        animator.ChangeStun();
        sePlayer.StunSE();
    }

    public void ClearStun()
    {
        IsStun = false;
        animator.ChangeNormal();
    }

    public async UniTask DeathPawn()
    {
        death = true;
        animator.ChangeNormal();
        animator.Death();
        sePlayer.DeathSE();
        await UniTask.Delay(1000, cancellationToken: destroyCancellationToken);
        MiniStatusDestroy();
        Destroy(gameObject);
        strage.RemovePawn(ID);
    }

    public virtual async UniTask<bool> Damage(float damage, int fromID)
    {
        AttackAble from = strage.GetPawnComponentByID<AttackAble>(fromID);
        await UniTask.WaitUntil(() => DamageAble, cancellationToken: token);

        var dis = from.Position - Position;
        if (Avoid) { sePlayer.DodgeSE(); DodgeEmote(dis); return false; }

        if (Burst) Guard = Mathf.Max(Guard, 50f);
        int d = status.Damage(damage*(1f - Guard * 0.01f));
        effectUnit.Damage(d);

        if(d >= MaxHP/3) sePlayer.BigDamageSE();
        else sePlayer.DamageSE();

        if(HP <= 0)
        {
            await UniTask.Delay(1000, cancellationToken: destroyCancellationToken);
            await DeathPawn();
            
        }

        return true;
    }

    public async UniTask<bool> Crash()
    {
        await UniTask.WaitUntil(() => DamageAble, cancellationToken: token);
        effectUnit.Damage(0);

        sePlayer.DamageSE();

        return true;
    }

    public async UniTask<bool> Heal(float heal)
    {
        int h = status.Heal(heal);
        effectUnit.Heal(h);
        return true;
    }

    public void Spell(int m)
    {
        mana = Mathf.Clamp(mana + m, 0, 9999);
    }

    public void UseMana(int m)
    {
        mana = Mathf.Clamp(mana - m, 0, 9999);
    }

    public void MiniStatusPrint() => miniStatusPrinter.PrintUI(id);
    public void MiniStatusDestroy() => miniStatusPrinter.DestroyUI(id);

    public abstract UniTask<IBattleCommand[]> AmbushSelect(AttackAble target);

    
}
