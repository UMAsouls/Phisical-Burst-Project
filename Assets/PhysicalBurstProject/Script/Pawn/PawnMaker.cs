using Cysharp.Threading.Tasks.Triggers;
using System;
using System.Collections;
using UnityEngine;
using Zenject;

[Serializable]
class PackageAndPos
{
    [SerializeField]
    public string packageID;

    [SerializeField]
    public GameObject positioner;
}

public class PawnMaker : MonoBehaviour
{
    [Inject]
    DiContainer container;

    [SerializeField]
    PackageAndPos[] PawnAndPos;

    [Inject]
    IPawnStrageable pawnStrageable;

    int id = 0;

    private void Awake()
    {
    }

    // Use this for initialization
    void Start()
    {
        GameObject[] pawns = new GameObject[PawnAndPos.Length];

        for(int i = 0; i < PawnAndPos.Length; i++)
        {
            GameObject pawn;
            pawn = container.InstantiatePrefab(PawnPackageStrage.instance.GetPawnPackage(PawnAndPos[i].packageID));

            var package = pawn.GetComponent<PawnPackage>();
            package.Position = PawnAndPos[i].positioner.transform.position;
            package.Init();
            var obj = package.MakePawn(container, id);
            id++;
            pawnStrageable.AddPawnObj(obj);

            Destroy(PawnAndPos[i].positioner);
        }

        pawnStrageable.IsSetComplete = true;
    }

    // Update is called once per frame
    void Update()
    {

    }
}