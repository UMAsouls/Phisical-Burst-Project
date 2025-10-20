using System.Collections.Generic;

public interface IObservable<out T>
{
    public void Subscribe(IObserver<T> observer);
}


