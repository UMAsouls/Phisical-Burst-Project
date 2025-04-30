using System.Collections;
using UnityEngine;

public interface SlotWindowControlable 
{
    public void ActionSet(string action, int idx);

    public void BurstAnim(int idx);

    public void FocusAnim(int idx);

    public void BlueFocusAnim(int idx);

    public void YellowFocusAnim(int idx);

    public void AnimEnd(int idx);
}