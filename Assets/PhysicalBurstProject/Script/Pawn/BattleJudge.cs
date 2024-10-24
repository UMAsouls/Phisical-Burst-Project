


public class BattleJudge
{
    public bool Judge(BattleCommandType self, BattleCommandType target, int priority)
    {
        if(self == BattleCommandType.Defence)
        {
            if(target == BattleCommandType.Strong && priority <= -2) return false;
        }

        if(self == BattleCommandType.Dodge)
        {
            if(target == BattleCommandType.Weak && priority <= -1) return false;
        }

        if(self == BattleCommandType.Strong)
        {
            if(target == BattleCommandType.Dodge) return false;
        }

        if(self == BattleCommandType.Weak)
        {
            if(target == BattleCommandType.Strong && priority <= 0) return false;
        }

        return true;
    }
}
