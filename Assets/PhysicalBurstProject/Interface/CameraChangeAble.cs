using System;
using UnityEngine;

public interface CameraChangeAble
{
    public void ChangeToPawnCamera(int pawnID);

    public void ChangeToMovableCamera();

    public OrthoCameraZoomAble GetZoomController();

    public bool IsSetComplete { get; }
}
