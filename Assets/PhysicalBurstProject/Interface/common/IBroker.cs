
public interface IBroker<V,T>
{
    public void Subscribe(V topic, ISubscriber<T> subscriber);

    public void Notify(V topic, T message);

}
