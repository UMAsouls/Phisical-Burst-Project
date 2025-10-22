
using System.Collections.Generic;
using Zenject;

public class Broker<V, T> : IBroker<V, T>, IInitializable
{
    Dictionary<V, List<ISubscriber<T>>> subscribers;

    public void BroadCast(V topic, T message)
    {
        if (!subscribers.ContainsKey(topic)) return;

        foreach (var subscriber in subscribers[topic]) subscriber.CatchMessage(message);
    }

    public void Initialize()
    {
        subscribers = new Dictionary<V, List<ISubscriber<T>>>();
    }

    public void Subscribe(V topic, ISubscriber<T> s)
    {
        if (subscribers.ContainsKey(topic))
        {
            subscribers[topic].Add(s);
        }
        else
        {
            subscribers[topic] = new List<ISubscriber<T>>();
            subscribers[topic].Add(s);
        }
    }
}
