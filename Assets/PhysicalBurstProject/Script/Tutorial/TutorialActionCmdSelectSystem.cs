using Cysharp.Threading.Tasks;
using System.Collections;
using UnityEngine;
using Zenject;

public class TutorialActionCmdSelectSystem : CmdSelectSystem
{

    [Inject] ITutorialSystem _system;

    public override async UniTask<bool> CmdSelect(int id)
    {
        await _system.Tutorial(TutorialTimingMessage.ActionCmdSelect, destroyCancellationToken);
        return await base.CmdSelect(id);
    }

    // Use this for initialization
    public override void Start()
    {
        _system.Init();
        base.Start();
    }
}