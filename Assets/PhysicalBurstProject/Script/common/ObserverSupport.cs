using System;
using System.Collections.Generic;

public class ObserverSupport
{
    public static void BroadCastMessage<T>(List<IObserver<T>> observers, T message)
    {
        foreach (var observer in observers) observer.OnNext(message);
    }
}
