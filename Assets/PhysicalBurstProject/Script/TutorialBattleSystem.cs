

using Cysharp.Threading.Tasks;
using Zenject;

public class TutorialBattleSystem : BattleSystem, ISubscriber<TutorialTimingMessage>
{

    [Inject]
    private IBroker<TutorialTopicFrag, TutorialTimingMessage> tutorialBroker;

    bool tutorialEnd = false;

    void Start()
    {
        tutorialBroker.Subscribe(TutorialTopicFrag.TutorialEnd, this);
        turn = 0;
        cts = this.GetCancellationTokenOnDestroy();
        Battle().Forget();
    }

    protected async UniTask TutorialWait()
    {
        tutorialEnd = false;
        await UniTask.WaitUntil(() => tutorialEnd, cancellationToken: destroyCancellationToken);
    }

    protected async UniTask Tutorial(TutorialTimingMessage message)
    {
        tutorialBroker.BroadCast(TutorialTopicFrag.TutorialStart, message);
        await TutorialWait();
    }

    protected override async UniTask TurnStart()
    {
        await Tutorial(TutorialTimingMessage.TurnStart);
        await base.TurnStart();
    }

    protected override async UniTask TurnEnd()
    {
        await Tutorial(TutorialTimingMessage.TurnEnd);
        await base.TurnEnd();
    }

    protected override async UniTask BattleStart()
    {
        await Tutorial(TutorialTimingMessage.BattleStart);
        await base.BattleStart();
    }

    public void CatchMessage(TutorialTimingMessage message)
    {
        tutorialEnd = true;
    }
}
