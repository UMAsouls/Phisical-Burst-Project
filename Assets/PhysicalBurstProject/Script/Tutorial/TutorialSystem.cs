using Cysharp.Threading.Tasks;
using Zenject;
using UnityEngine;
using System.Threading;

public class TutorialSystem : ITutorialSystem, ISubscriber<TutorialTimingMessage>
{
    [Inject]
    private IBroker<TutorialTopicFrag, TutorialTimingMessage> tutorialBroker;

    bool tutorialEnd = false;

    bool inited = false;

    public async UniTask Tutorial(TutorialTimingMessage message, CancellationToken token)
    {
        tutorialEnd = false;
        tutorialBroker.BroadCast(TutorialTopicFrag.TutorialStart, message);
        await UniTask.WaitUntil(() => tutorialEnd, cancellationToken: token);
    }

    public void Init()
    {
        if(inited) return;
        tutorialBroker.Subscribe(TutorialTopicFrag.TutorialEnd, this);
        inited = true;
    }

    public void CatchMessage(TutorialTimingMessage message)
    {
        tutorialEnd = true;
    }
}
