

using System.Collections.Generic;

public interface IObserver<in T>
{
    public void OnComplete();
    public void OnNext(T value);
}
