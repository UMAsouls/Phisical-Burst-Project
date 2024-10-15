
public class HealCommand : ActionCommand<IHealCommand>, IHealCommand
{
    private float range;
    public float Range => range;

    private float heal;
    public float Heal => heal;

    public override ActionCmdType Type => ActionCmdType.Heal;
}
