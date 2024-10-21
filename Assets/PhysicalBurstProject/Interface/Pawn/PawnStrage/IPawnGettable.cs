using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPawnGettable
{
    public T[] GetPawnList<T>();

    public T GetPawnByID<T>(int id);

    public CinemachineVirtualCamera GetPawnCameraByID(int id);

    public bool IsSetComplete { get; }
}
