using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class BattlePawnStrage : IPawnStrageable, IPawnGettable
{
    private Dictionary<int, GameObject> pawnDict;

    private bool isSetComplete;

    public BattlePawnStrage()
    {
        pawnDict = new Dictionary<int, GameObject>();
        isSetComplete = false;
    }

    public bool IsSetComplete => isSetComplete;

    bool IPawnStrageable.IsSetComplete { set => isSetComplete = value; }

    public void AddPawnObj(GameObject obj)
    {
        var idgettable = obj.GetComponent<IDGettable>();
        Assert.AreNotEqual(null, idgettable);
        pawnDict[idgettable.ID] = obj;
    }

    public T GetPawnByID<T>(int id)
    {
        return pawnDict[id].GetComponent<T>();
    }

    public CinemachineVirtualCamera GetPawnCameraByID(int id)
    {
        return pawnDict[id].GetComponentInChildren<CinemachineVirtualCamera>();
    }

    public T[] GetPawnList<T>()
    {
        T[] values = new T[pawnDict.Count];

        int idx = 0;
        foreach (var pair in pawnDict)
        {
            T comp = pair.Value.GetComponent<T>();
            values[idx++] = comp;
        }
        return values;
    }
}
