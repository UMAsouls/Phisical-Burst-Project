
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Broker<V, T> : IBroker<V, T>
{
    Dictionary<V, HashSet<ISubscriber<T>>> subscribers;

    public void BroadCast(V topic, T message)
    {
        if (subscribers == null)
        {
            Debug.Log("no one subscribe"); return;
        }
        if (!subscribers.ContainsKey(topic)) return;

        foreach (var subscriber in subscribers[topic]) subscriber.CatchMessage(message);
    }

    public void Subscribe(V topic, ISubscriber<T> s)
    {
        if(subscribers == null) subscribers = new Dictionary<V, HashSet<ISubscriber<T>>>();

        if (!subscribers.ContainsKey(topic))
        {
            subscribers[topic] = new HashSet<ISubscriber<T>>();
        }
        subscribers[topic].Add(s);
    }

    public bool UnSubscribe(V topic, ISubscriber<T> subscriber)
    {
        if (subscriber == null) return false;
        if (!subscribers.ContainsKey(topic)) return false;

        subscribers[topic].Remove(subscriber);
        return true;
    }
}
