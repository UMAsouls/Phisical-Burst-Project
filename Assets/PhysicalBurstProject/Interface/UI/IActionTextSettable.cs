

public interface IActionTextSettable: StringSetable
{
    public float FontSize { set; get; }
    public float TextWidth { get; }

    public void SizeUpdate();
}
