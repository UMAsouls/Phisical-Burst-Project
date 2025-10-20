
using Codice.Client.BaseCommands;
using System.Collections.Generic;
using UnityEngine;

public class CommonBroker<V, T> : MonoBehaviour, IBroker<V, T>
{
    private Dictionary<V, List<ISubscriber<T>>> subscribers;

    public void Notify(V topic, T message)
    {
        foreach(var subscriber in subscribers[topic])
        {
            subscriber.Update(message);
        }
    }

    public void Subscribe(V topic, ISubscriber<T> s)
    {
        if (!subscribers.ContainsKey(topic)) subscribers[topic] = new List<ISubscriber<T>>();
        subscribers[topic].Add(s);
    }

    public void Awake()
    {
        subscribers = new Dictionary<V, List<ISubscriber<T>>>();
    }
}
