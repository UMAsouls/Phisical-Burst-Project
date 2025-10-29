using System.Collections;
using UnityEngine;
using Zenject;

public class PawnStatusPrinter :MonoBehaviour, ISubscriber<StatusUIMessage>
{

    [Inject]
    IBroker<UIControlTopic, StatusUIMessage> broker;

    [Inject]
    Canvas canvas;

    [Inject]
    DiContainer container;

    [SerializeField]
    GameObject pawnStatusUI;

    private GameObject printedUI;

    public void CatchMessage(StatusUIMessage message)
    {
        if (message.Destroy) DestroyUI();
        else PrintUI(message.Status);
    }

    GameObject PrintUIInCanvas(GameObject ui)
    {
        var obj = container.InstantiatePrefab(ui);
        obj.transform.SetParent(canvas.transform, false);

        return obj;
    }

    void PrintUI(IStatus status)
    {
        var obj = PrintUIInCanvas(pawnStatusUI);
        var setter = obj.GetComponent<IPawnStatusUISetter>();
        setter.SetPawnStatus(status);
        printedUI = obj;
    }

    void DestroyUI()
    {
        if (printedUI == null) return;
        Destroy(printedUI);
        printedUI = null;
    }

    // Use this for initialization
    void Start()
    {
        broker.Subscribe(UIControlTopic.PawnStatus, this);
    }

    // Update is called once per frame
    void Update()
    {

    }
}