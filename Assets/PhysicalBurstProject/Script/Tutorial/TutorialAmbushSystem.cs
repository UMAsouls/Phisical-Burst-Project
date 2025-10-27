using Cysharp.Threading.Tasks;
using System.Collections;
using UnityEngine;
using Zenject;

public class TutorialAmbushSystem : AmbushSelectSystem
{
    [Inject] ITutorialSystem tutorialSystem;

    public override async UniTask<bool> AmbushSelect(int pawnID)
    {
        await tutorialSystem.Tutorial(TutorialTimingMessage.Ambush, destroyCancellationToken);
        return await base.AmbushSelect(pawnID);
    }

    public override void Start()
    {
        tutorialSystem.Init();
        base.Start();
    }
}