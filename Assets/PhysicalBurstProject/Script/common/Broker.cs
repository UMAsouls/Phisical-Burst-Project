
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Broker<V, T> : IBroker<V, T>
{
    Dictionary<V, List<ISubscriber<T>>> subscribers;

    public void BroadCast(V topic, T message)
    {
        Debug.Log(topic);
        Debug.Log(message.ToString());
        if (!subscribers.ContainsKey(topic)) return;

        foreach (var subscriber in subscribers[topic]) subscriber.CatchMessage(message);
        Debug.Log($"{topic}: Sended");
    }

    public void Subscribe(V topic, ISubscriber<T> s)
    {
        if(subscribers == null) subscribers = new Dictionary<V, List<ISubscriber<T>>>();

        if (!subscribers.ContainsKey(topic))
        {
            subscribers[topic] = new List<ISubscriber<T>>();
        }
        subscribers[topic].Add(s);
    }
}
