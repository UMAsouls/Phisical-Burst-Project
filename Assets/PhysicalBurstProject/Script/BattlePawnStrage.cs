using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class BattlePawnStrage : IPawnStrageable, IPawnGettable
{
    private Dictionary<int, GameObject> pawnDict;

    private List<EnemyAI> enemyAIs;

    private bool isSetComplete;

    public BattlePawnStrage()
    {
        pawnDict = new Dictionary<int, GameObject>();
        isSetComplete = false;
    }

    public bool IsSetComplete => isSetComplete;

    bool IPawnStrageable.IsSetComplete { set => SetComplete(value); }

    public void SetComplete(bool value)
    {
        isSetComplete = value;
        if (!isSetComplete) return;

        enemyAIs = new List<EnemyAI>();
        foreach (var item in pawnDict.Values)
        {
            if (item.GetComponent<PawnTypeGettable>().Type == PawnType.Enemy)
            {
                enemyAIs.Add(item.GetComponent<EnemyAI>());
            }
        }
    }
    

    public void HateBroadCast(float hate, int from)
    {
        foreach (var e in enemyAIs) e.HateAdd(from, hate);
    }

    public void AddPawnObj(GameObject obj)
    {
        var idgettable = obj.GetComponent<IDGettable>();
        Assert.AreNotEqual(null, idgettable);
        pawnDict[idgettable.ID] = obj;
    }

    public T GetPawnByID<T>(int id)
    {
        if (pawnDict.ContainsKey(id)) return pawnDict[id].GetComponent<T>();
        else return default;
    }

    public void RemovePawn(int id)
    {
        pawnDict.Remove(id);
    }

    public CinemachineVirtualCamera GetPawnCameraByID(int id)
    {
        return pawnDict[id].GetComponentInChildren<CinemachineVirtualCamera>();
    }

    public List<T> GetPawnsInArea<T>(Vector2 point, float range)
    {
        List<T> list = new List<T>();

        foreach (var pair in pawnDict)
        {
            PosGetPawn p = pair.Value.GetComponent<PosGetPawn>();
            if((p.Position - point).magnitude <= range)
            {
                list.Add(pair.Value.GetComponent<T>());
            }
        }

        return list;
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
