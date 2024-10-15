using Cysharp.Threading.Tasks;
using System.Collections;
using UnityEngine;

public class HealMaker : CommandMakerBase<IHealCommand>
{

    public override async UniTask<IActionCommandBehaviour> MakeBehaviour(IHealCommand cmd)
    {
        await UniTask.WaitUntil(() => isConfirm | isCancel);
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