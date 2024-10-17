using Cysharp.Threading.Tasks;
using System.Collections;
using UnityEngine;

public class HealMaker : CommandMakerBase<IHealCommand>
{

    [SerializeField]
    private GameObject RangeCircle;

    public override async UniTask<IActionCommandBehaviour> MakeBehaviour(IHealCommand cmd , int pawnID)
    {
        var vpawn = strage.GetPawnById<IVirtualPawn>(pawnID);
        var obj = Instantiate(RangeCircle, (Vector3)(vpawn.VirtualPos), Quaternion.identity);

        await UniTask.WaitUntil(() => isConfirm | isCancel);

        Destroy(obj);
        if (isConfirm) { return new HealBehaviour(cmd); }
        else { return null; }
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}