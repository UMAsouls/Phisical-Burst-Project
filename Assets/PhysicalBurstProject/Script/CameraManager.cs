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

    private float firstCameraSize;

    private bool isSetComplete;

    public bool IsSetComplete => isSetComplete;

    private void ChangeCameraTo(CinemachineVirtualCamera camera)
    {
        if(main != null)
        {
            main.Priority = -1;
            main.m_Lens.OrthographicSize = firstCameraSize;
        }
        main = camera;
        main.Priority = 5;
        firstCameraSize = main.m_Lens.OrthographicSize;
    }

    public void ChangeToPawnCamera(int pawnID)
    {
        ChangeCameraTo(pawnCameras[pawnID]);
    }

    public void ChangeToSelectPhazeCamera()
    {
        ChangeCameraTo(selectPhazeCamera);
    }

    public OrthoCameraZoomAble GetZoomController()
    {
        if(main == null) return null;
        return main.gameObject.GetComponent<OrthoCameraZoomAble>();
    }

    // Use this for initialization
    async void Start()
    {
        pawnCameras = new Dictionary<int, CinemachineVirtualCamera>();

        await UniTask.WaitUntil(() => strage.IsSetComplete);
        IDGettable[] list = strage.GetPawnList<IDGettable>();
        foreach (IDGettable item in list)
        {
            Debug.Log("camera add: " + item.ID);
            pawnCameras.Add(item.ID, strage.GetPawnCameraByID(item.ID));
        }

        Debug.Log("camera set complete");
        isSetComplete = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    
}