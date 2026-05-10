using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IBroker<V,T>
{
    void Subscribe(V topic, ISubscriber<T> subscriber);
    bool UnSubscribe(V topic, ISubscriber<T> subscriber);
    void BroadCast(V topic, T message);
}
