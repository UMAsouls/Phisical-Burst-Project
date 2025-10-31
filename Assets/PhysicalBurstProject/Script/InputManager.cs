using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

[RequireComponent(typeof(PlayerInput))]
[DefaultExecutionOrder(-10)]
public class InputManager : MonoBehaviour, ISubscriber<InputMode>, ISubscriber<ActionSetMessage>
{
    [Inject]
    private IBroker<InputModeTopic, InputMode> broker;

    [Inject]
    private IBroker<ActionSetTopic, ActionSetMessage> actionBroker;

    private Dictionary<
        InputMode,
        Dictionary<string, Action<InputAction.CallbackContext>>
        > settedAction;

    private PlayerInput input;

    public void CatchMessage(InputMode message)
    {
        input.SwitchCurrentActionMap(Enum.GetName(typeof(InputMode), message));
    }

    public void CatchMessage(ActionSetMessage message)
    {
        SetActionMessage(message);
    }

    protected void RemoveAllAction()
    {
        foreach(var (mode, dict) in settedAction)
        {
            foreach (var (action, func) in dict)
            {
                ResetAction(mode, action, func);
            }
        }
    }

    protected void ResetAction(InputMode mode, string action, Action<InputAction.CallbackContext> func)
    {
        string type = Enum.GetName(typeof(InputMode), mode);

        var act = input.actions.FindActionMap(type).FindAction(action);

        if (!settedAction.ContainsKey(mode)) return;
        if (!settedAction[mode].ContainsKey(action)) return;

        act.performed -= func;
        act.canceled -= func;

        act.Reset();
    }

    protected void RemoveAction(InputMode mode, string action, Action<InputAction.CallbackContext> func)
    {
        string type = Enum.GetName(typeof(InputMode), mode);

        var act = input.actions.FindActionMap(type).FindAction(action);

        if (!settedAction.ContainsKey(mode)) return;
        if (!settedAction[mode].ContainsKey(action)) return;

        act.performed -= func;
        act.canceled -= func;

    }

    protected void SetAction(InputMode mode,  string action, Action<InputAction.CallbackContext> func)
    {
        string type = Enum.GetName(typeof(InputMode), mode);
        var act = input.actions.FindActionMap(type).FindAction(action);

        if (!settedAction.ContainsKey(mode))
        {
            settedAction[mode] =
                new Dictionary<string, Action<InputAction.CallbackContext>>();
        }

        act.canceled += func;
        act.performed += func;
    }

    protected void SetActionMessage(ActionSetMessage message)
    {
        if (input == null) return;
        if (message.Remove)
        {
            RemoveAction(message.Type, message.Action, message.Function);
            settedAction[message.Type].Remove(message.Action);
        }
        else
        {
            SetAction(message.Type, message.Action, message.Function);
            settedAction[message.Type][message.Action] = message.Function;
        }
    }

    private void TestCall(InputAction.CallbackContext context)
    {
        Debug.Log($"Action call: {GetInstanceID()}, input id: {input.GetInstanceID()}");
    } 

    private void CheckEventBindings(Action<InputAction.CallbackContext> actionEvent, string actname)
    {
        Delegate[] ds = actionEvent.GetInvocationList();
        string bind_m = "";
        foreach (Delegate d in ds) bind_m += d.Method.Name + " ,";
        Debug.Log($"set: {actname}: {bind_m}");
    }

    void Awake()
    {
        broker.Subscribe(InputModeTopic.SwitchActionMap, this);
        actionBroker.Subscribe(ActionSetTopic.SetAction, this);
        input = GetComponent<PlayerInput>();
        settedAction = new Dictionary<
            InputMode,
            Dictionary<string, Action<InputAction.CallbackContext>>
            >();
    }

    void OnDisable()
    {
        RemoveAllAction();
    }

    void OnDestroy()
    {
        broker.UnSubscribe(InputModeTopic.SwitchActionMap, this);
        actionBroker.UnSubscribe(ActionSetTopic.SetAction, this);
        Debug.Log("Destroy Input Manager");
        Destroy(gameObject);
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