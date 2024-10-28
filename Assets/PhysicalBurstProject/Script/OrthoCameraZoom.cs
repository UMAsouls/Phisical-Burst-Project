using Cinemachine;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

public class OrthoCameraZoom : CinemachineExtension, OrthoCameraZoomAble
{
    [SerializeField]
    [Range(0.1f, 10f)]
    private float zoomSpeed;

    private float firstSpeed;

    public float ZoomSpeed { get => zoomSpeed; set => zoomSpeed = value; }

    [SerializeField]
    private float minOrthoSize = 2.0f;

    [SerializeField]
    private float maxOrthoSize = 12f;

    private float targetOrthoSize;

    private float befOrthoSize;

    private float t;

    public float OrthoSize { get => targetOrthoSize; set => SetZoom(value); }

    private float firstOrthoSize;

    public void ZoomSpeedInit()
    {
        zoomSpeed = firstSpeed;
    }

    public void ZoomInit()
    {
        GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize = firstOrthoSize;
        SetZoom(firstOrthoSize);
    }

    private void SetZoom(float orthoSize)
    {
        befOrthoSize = GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize;
        targetOrthoSize = Mathf.Clamp(orthoSize, minOrthoSize, maxOrthoSize);
        t = 0;
    }

    protected override void Awake()
    {
        base.Awake();
        // CinemachineVirtualCameraの現在のOrthoSizeを取得
        targetOrthoSize = GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize;
        firstSpeed = zoomSpeed;
        firstOrthoSize = targetOrthoSize;
    }

    protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if(stage != CinemachineCore.Stage.Aim) return;

        var lens = state.Lens;
        t += zoomSpeed*deltaTime;

        lens.OrthographicSize = Mathf.Lerp(befOrthoSize, targetOrthoSize, t);
        state.Lens = lens;
    }


}