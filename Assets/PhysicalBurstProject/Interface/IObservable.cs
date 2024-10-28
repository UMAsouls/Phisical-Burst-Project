

public interface IObservable<out T>
{
    public void Subscribe(IObserver<T> observer);
}
