

using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

public class TutorialBattleSystem : BattleSystem, ISubscriber<TutorialTimingMessage>
{

    [Inject]
    private IBroker<TutorialTopicFrag, TutorialTimingMessage> tutorialBroker;

    bool tutorialEnd = false;

    protected override void Start()
    {
        tutorialBroker.Subscribe(TutorialTopicFrag.TutorialEnd, this);
        base.Start();
    }

    protected async UniTask Tutorial(TutorialTimingMessage message)
    {
        tutorialEnd = false;
        tutorialBroker.BroadCast(TutorialTopicFrag.TutorialStart, message);
        await UniTask.WaitUntil(() => tutorialEnd, cancellationToken: destroyCancellationToken);
    }

    protected override async UniTask TurnStart()
    {
        await base.TurnStart();
        await Tutorial(TutorialTimingMessage.TurnStart);
    }

    protected override async UniTask TurnEnd()
    {
        await base.TurnEnd();
        await Tutorial(TutorialTimingMessage.TurnEnd);
    }

    protected override async UniTask BattleStart()
    {
        await base.BattleStart();
        await Tutorial(TutorialTimingMessage.BattleStart);
    }

    protected override async UniTask Select(ActionSelectable pawn)
    {
        await Tutorial(TutorialTimingMessage.Select);
        await base.Select(pawn);
    }

    public void CatchMessage(TutorialTimingMessage message)
    {
        tutorialEnd = true;
    }
}
