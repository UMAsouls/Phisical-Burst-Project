using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

[RequireComponent(typeof(Canvas))]
public class TutorialContoller : ConfirmCancelCatchAble, ISubscriber<TutorialTimingMessage>
{

    [Inject]
    IBroker<TutorialTopicFrag, TutorialTimingMessage> tutorialBroker;

    [Inject]
    DiContainer container;

    [SerializeField]
    private SerializedDictionary<TutorialTimingMessage, List<GameObject>> tutorialUI;

    private Dictionary<TutorialTimingMessage, int> ui_indices;

    private ITutorialUI printingUI;
    private TutorialTimingMessage printingUIKind;

    protected override InputMode SelfMode => InputMode.Tutorial;

    public override void OnConfirm(InputAction.CallbackContext context)
    {
        if(context.canceled) return;
        OnNext();
        systemSEPlayer.ConfirmSE();
    }

    public override void OnCancel(InputAction.CallbackContext context)
    {
        if (context.canceled) return;
        OnPrevious();
        systemSEPlayer.ConfirmSE();
    }

    public void OnNext()
    {
        if (printingUI == null) return;
        var end = printingUI.NextUI();
        if(end)
        {
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

    protected override void Awake()
    {
        tutorialBroker.Subscribe(TutorialTopicFrag.TutorialStart, this);
        ui_indices = new Dictionary<TutorialTimingMessage, int>();
        foreach (var key in tutorialUI.Keys) ui_indices[key] = 0;

        base.Awake();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CatchMessage(TutorialTimingMessage message)
    {
        printingUIKind = message;

        if(!tutorialUI.ContainsKey(message))
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

        InputModeChangeToSelf();

    }
}