using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Canvas))]
public class TutorialContoller : MonoBehaviour, ISubscriber<TutorialTimingMessage>
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

    public void OnNext()
    {
        if (printingUI == null) return;
        var end = printingUI.NextUI();
        if(end) tutorialBroker.BroadCast(TutorialTopicFrag.TutorialEnd, printingUIKind);
    }

    public void OnPrevious()
    {
        if (printingUI == null) return;
        var start = printingUI.PreviousUI();
    }

    // Use this for initialization
    void Start()
    {
        tutorialBroker.Subscribe(TutorialTopicFrag.TutorialStart, this);
        ui_indices = new Dictionary<TutorialTimingMessage, int>();
        foreach (var key in tutorialUI.Keys) ui_indices[key] = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CatchMessage(TutorialTimingMessage message)
    {
        printingUIKind = message;
        var obj = tutorialUI[message][ui_indices[message]++];
        if(obj == null) tutorialBroker.BroadCast(TutorialTopicFrag.TutorialEnd, message);

        var printingobj = container.InstantiatePrefab(obj);
        printingobj.transform.SetParent(gameObject.transform, false);

        printingUI = printingobj.GetComponent<ITutorialUI>();
        if(printingUI == null) tutorialBroker.BroadCast(TutorialTopicFrag.TutorialEnd, message);

        
    }
}