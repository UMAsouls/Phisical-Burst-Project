using Cysharp.Threading.Tasks;
using System.Collections;
using UnityEngine;
using Zenject;

public class TutorialBattleCmdActionSelectSystem : BattleCmdActionSelectSystem
{

    [Inject] ITutorialSystem _system;

    public override async UniTask<bool> Select(int pawnID)
    {
        await _system.Tutorial(TutorialTimingMessage.BattleCmdSelect, destroyCancellationToken);
        return await base.Select(pawnID);
    }

    // Use this for initialization
    public override void Start()
    {
        _system.Init();
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {

    }
}