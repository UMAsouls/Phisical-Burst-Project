using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

[RequireComponent(typeof(Canvas), typeof(CanvasGroup))]
public class TutorialContoller : ConfirmCancelCatchAble, ISubscriber<TutorialTimingMessage>
{

    [Inject]
    IBroker<TutorialTopicFrag, TutorialTimingMessage> tutorialBroker;

    [Inject]
    DiContainer container;

    [SerializeField]
    private float FadeSpeed = 0.001f;

    [SerializeField]
    private SerializedDictionary<TutorialTimingMessage, List<GameObject>> tutorialUI;

    private Dictionary<TutorialTimingMessage, int> ui_indices;

    private CanvasGroup canvasGroup;

    private ITutorialUI printingUI;
    private TutorialTimingMessage printingUIKind;

    protected override InputMode SelfMode => InputMode.Tutorial;

    private static float fadeWaitTime = 1000 / 60;

    public override void OnConfirm(InputAction.CallbackContext context)
    {
        if(!context.performed) return;
        OnNext();
        systemSEPlayer.ConfirmSE();
    }

    public override void OnCancel(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        OnPrevious();
        systemSEPlayer.ConfirmSE();
    }

    public async void OnNext()
    {
        if (printingUI == null) return;
        var end = printingUI.NextUI();
        if(end)
        {
            await FadeOut();
            printingUI.DestroyUI();
            printingUI = null;
            tutorialBroker.BroadCast(TutorialTopicFrag.TutorialEnd, printingUIKind);
        }
    }

    public void OnPrevious()
    {
        if (printingUI == null) return;
        var start = printingUI.PreviousUI();
    }

    private async UniTask FadeIn()
    {
        float alpha = 0f;
        while(alpha < 1f)
        {
            alpha += FadeSpeed;
            canvasGroup.alpha = alpha;
            await UniTask.Delay((int)fadeWaitTime);
        }
    }

    private async UniTask FadeOut()
    {
        float alpha = 1f;
        while (alpha > 0f)
        {
            alpha -= FadeSpeed;
            canvasGroup.alpha = alpha;
            await UniTask.Delay((int)fadeWaitTime);
        }
    }

    protected override void Awake()
    {
        tutorialBroker.Subscribe(TutorialTopicFrag.TutorialStart, this);
        ui_indices = new Dictionary<TutorialTimingMessage, int>();
        foreach (var key in tutorialUI.Keys) ui_indices[key] = 0;

        base.Awake();
    }

    public override void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public async void CatchMessage(TutorialTimingMessage message)
    {
        Debug.Log(message);

        printingUIKind = message;

        if(!tutorialUI.ContainsKey(message))
        {
            tutorialBroker.BroadCast(TutorialTopicFrag.TutorialEnd, message);
            return;
        }
        if (ui_indices[message] >= tutorialUI[message].Count)
        {
            tutorialBroker.BroadCast(TutorialTopicFrag.TutorialEnd, message);
            return;
        }
        var obj = tutorialUI[message][ui_indices[message]++];
        if (obj == null)
        {
            tutorialBroker.BroadCast(TutorialTopicFrag.TutorialEnd, message);
            return;
        }

        var printingobj = container.InstantiatePrefab(obj);
        printingobj.transform.SetParent(gameObject.transform, false);

        printingUI = printingobj.GetComponent<ITutorialUI>();
        if(printingUI == null)
        {
            tutorialBroker.BroadCast(TutorialTopicFrag.TutorialEnd, message);
            return;
        }

        await FadeIn();

        InputModeChangeToSelf();

    }
}