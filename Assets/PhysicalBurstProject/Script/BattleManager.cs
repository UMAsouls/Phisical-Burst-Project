using IceMilkTea.StateMachine;
using UnityEngine;
using SUS;

public class BattleManager : Singleton<BattleManager>
{

    [SerializeField]
    private AStateSencer<BattleStateID>[] _stateSencers;

    protected static AStateSencer<BattleStateID>[] stateSencers;

    protected ImtStateMachine<BattleManager, BattleStateEventID> stateMachine;


    protected static BattleStateID START_STATE;

    protected abstract class StateBase : ImtStateMachine<BattleManager, BattleStateEventID>.State
    {
        protected abstract BattleStateID mode { get; }

        protected internal override void Enter()
        {
            foreach (var sencer in stateSencers) {
                sencer.StateChange(mode);
            }
        }
    }

    protected class StartState : StateBase
    {
        protected override BattleStateID mode => BattleStateID.StartState;
    }

    protected class TurnStartState : StateBase
    {
        protected override BattleStateID mode => BattleStateID.TurnStartState;
    }

    protected class SelectState : StateBase
    {
        protected override BattleStateID mode => BattleStateID.SelectState;
    }

    protected class ActionState : StateBase
    {
        protected override BattleStateID mode => BattleStateID.ActionState;
    }

    protected class BattleState : StateBase
    {
        protected override BattleStateID mode => BattleStateID.BattleState;
    }

    protected class TurnEndState : StateBase
    {
        protected override BattleStateID mode => BattleStateID.TurnEndState;
    }

    protected class EndState : StateBase
    {
        protected override BattleStateID mode => BattleStateID.EndState;
    }

    protected override void Awake()
    {
        base.Awake();
        stateSencers = _stateSencers;

        stateMachine.SetStartState<StartState>();

        stateMachine.AddTransition<StartState, TurnStartState>(BattleStateEventID.TurnStart);
        stateMachine.AddTransition<TurnStartState, SelectState>(BattleStateEventID.SelectStart);
        stateMachine.AddTransition<SelectState,ActionState>(BattleStateEventID.ActionStart);
        stateMachine.AddTransition<ActionState,BattleState>(BattleStateEventID.BattleStart);
        stateMachine.AddTransition<BattleState, ActionState>(BattleStateEventID.BattleEnd);
        stateMachine.AddTransition<ActionState, TurnEndState>(BattleStateEventID.ActionEnd);
        stateMachine.AddTransition<TurnEndState, TurnStartState>(BattleStateEventID.TurnStart);
        stateMachine.AddTransition<TurnEndState, EndState>(BattleStateEventID.End);

        
    }

    // Start is called before the first frame update
    void Start()
    {
        stateMachine.Update();
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.Update();
    }
}
