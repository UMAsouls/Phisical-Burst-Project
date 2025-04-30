using System.Collections;
using UnityEngine;

public interface IBattleCmdSlotUIPrinter: IUIPrinter<SlotWindowControlable>
{
    public SlotWindowControlable PrintUIAt(Vector2 pos);

    public SlotWindowControlable PrintUILeft();

    public SlotWindowControlable PrintUIRight();

    public SlotWindowControlable PrintUIInBattle(Vector2 worldPoint, Vector2 dir);
}