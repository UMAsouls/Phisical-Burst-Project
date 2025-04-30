using UnityEngine;

public interface SelectedPawn: IDGettable, PawnTypeGettable
{
    public void SelectedFocus();

    public void SelectedUnFocus();
}
