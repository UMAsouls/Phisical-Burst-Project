using Cinemachine;
using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using Zenject;

public class CameraManager : MonoBehaviour,  CameraChangeAble
{
    [Inject]
    private IPawnGettable strage;

    private Dictionary<int, CinemachineVirtualCamera> pawnCameras;

    [SerializeField]
    private CinemachineVirtualCamera selectPhazeCamera;

    private CinemachineVirtualCamera main;

    private void ChangeCameraTo(CinemachineVirtualCamera camera)
    {
        if(main != null) main.Priority = -1;
        main = camera;
        main.Priority = 5;
    }

    public void ChangeToPawnCamera(int pawnID)
    {
        ChangeCameraTo(pawnCameras[pawnID]);
    }

    public void ChangeToSelectPhazeCamera()
    {
        ChangeCameraTo(selectPhazeCamera);
    }

    // Use this for initialization
    async void Start()
    {
        pawnCameras = new Dictionary<int, CinemachineVirtualCamera>();

        await UniTask.WaitUntil(() => strage.IsSetComplete);
        IDGettable[] list = strage.GetPawnList<IDGettable>();
        foreach (IDGettable item in list)
        {
            pawnCameras.Add(item.ID, strage.GetPawnCameraByID(item.ID));
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}