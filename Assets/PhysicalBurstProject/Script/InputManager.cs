using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

[RequireComponent(typeof(PlayerInput))]
public class InputManager : MonoBehaviour, ISubscriber<InputMode>, ISubscriber<ActionSetMessage>
{
    [Inject]
    private IBroker<InputModeTopic, InputMode> broker;

    [Inject]
    private IBroker<ActionSetTopic, ActionSetMessage> actionBroker;

    PlayerInput input;

    public void CatchMessage(InputMode message)
    {
        Debug.Log(message);
        input.SwitchCurrentActionMap(Enum.GetName(typeof(InputMode), message));
        Debug.Log(input.actions.ToString());
    }

    public void CatchMessage(ActionSetMessage message)
    {
        SetAction(message);
    }

    protected void SetAction(ActionSetMessage message)
    {
        string type = Enum.GetName(typeof(InputMode), message.Type);
        var currentMap = input.currentActionMap.name;
        input.SwitchCurrentActionMap(type);
        if (message.Remove)
        {
            input.actions[message.Action].performed -= message.Function;
            input.actions[message.Action].canceled -= message.Function;
        }
        else
        {
            input.actions[message.Action].canceled += message.Function;
            input.actions[message.Action].performed += message.Function;
        }
        input.SwitchCurrentActionMap(currentMap);
    }

    void Awake()
    {
        broker.Subscribe(InputModeTopic.SwitchActionMap, this);
        actionBroker.Subscribe(ActionSetTopic.SetAction, this);
        Debug.Log("InputManager: Subscribe");
        input = GetComponent<PlayerInput>();
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