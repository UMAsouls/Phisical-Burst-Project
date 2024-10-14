

using Cysharp.Threading.Tasks;

public class CommandAction : IAction
{
    public ActionType Type => ActionType.Action;

    private IActionCommand command;

    private IActionCommandBehaviour behaviour;

    public CommandAction(IActionCommand cmd, IActionCommandBehaviour behaviour)
    {
        command = cmd;
        this.behaviour = behaviour;
    }

    public bool CancelAct(ActionSettable pawn)
    {
        throw new System.NotImplementedException();
    }

    public UniTask DoAct(ActablePawn pawn)
    {
        throw new System.NotImplementedException();
    }

    public bool setAct(ActionSettable pawn)
    {
        throw new System.NotImplementedException();
    }
}
