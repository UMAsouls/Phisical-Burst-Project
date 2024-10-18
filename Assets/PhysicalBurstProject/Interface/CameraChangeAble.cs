using System;
using UnityEngine;

public interface CameraChangeAble
{
    public void ChangeToPawnCamera(int pawnID);

    public void ChangeToSelectPhazeCamera();

    public OrthoCameraZoomAble GetZoomController();

    public bool IsSetComplete { get; }
}
