

public interface ISlotTextSettable: StringSetable
{
    public float FontSize { set; get; }
    public float TextWidth { get; }

    public void SizeUpdate();
}
