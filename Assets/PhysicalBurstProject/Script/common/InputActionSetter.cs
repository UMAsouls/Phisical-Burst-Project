using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public abstract class InputActionSetter : MonoBehaviour
{

    private Dictionary<string, Action<InputAction.CallbackContext>> settedAction;

    [Inject]
    protected IBroker<ActionSetTopic, ActionSetMessage> actionSetBroker;

    [Inject]
    protected IBroker<InputModeTopic, InputMode> inputModeBroker;

    protected void InputModeChangeToSelf() => inputModeBroker.BroadCast(InputModeTopic.SwitchActionMap, SelfMode);

    protected abstract InputMode SelfMode { get; }

    protected void SetAction(
        string action, Action<InputAction.CallbackContext> func
    )
    {
        var act = new ActionSetMessage(SelfMode, action, func);
        actionSetBroker.BroadCast(ActionSetTopic.SetAction, act);
        settedAction[action] = func;
    }

    protected virtual void Awake()
    {
        settedAction = new Dictionary<string, Action<InputAction.CallbackContext>>();
    }

    protected abstract void SetAllAction();

    private void RemoveAllAction()
    {
        foreach (var (action, func) in settedAction)
        {
            var act = new ActionSetMessage(SelfMode, action, func, true);
            actionSetBroker.BroadCast(ActionSetTopic.SetAction, act);
        }
    }

    // Use this for initialization
    public virtual void Start()
    {
    }

    protected virtual void OnEnable()
    {
        SetAllAction();
    }

    protected virtual void OnDisable()
    {
        RemoveAllAction();
    }

    protected virtual void OnDestroy()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}