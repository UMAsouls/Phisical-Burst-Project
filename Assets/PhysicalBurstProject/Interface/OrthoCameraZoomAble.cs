using UnityEngine;

public interface OrthoCameraZoomAble
{
    public float OrthoSize { set; get; }

    public float ZoomSpeed { set; get; }

    public void ZoomSpeedInit();

    public void ZoomInit();
}
