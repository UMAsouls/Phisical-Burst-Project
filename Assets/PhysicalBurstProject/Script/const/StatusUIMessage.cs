

public struct StatusUIMessage
{
    public StatusUIMessage(bool destroy,  IStatus status)
    {
        Destroy = destroy;
        Status = status;
    }

    public bool Destroy { get; }
    public IStatus Status { get; }
}
