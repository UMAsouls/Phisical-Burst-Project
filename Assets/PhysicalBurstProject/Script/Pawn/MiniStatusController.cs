using System.Collections;
using UnityEngine;
using Zenject;

public class MiniStatusController : MonoBehaviour, IMiniStatusController
{

    [Inject]
    private MiniStatusPrinter printer;

    private IStatus status;
    private IPawnBattleInfo info;

    public void MiniStatusPrint() => printer.PrintUI(info.ID, status);

    public void MiniStatusDestroy() => printer.DestroyUI(info.ID);

    // Use this for initialization
    void Start()
    {
        info = GetComponent<IPawnBattleInfo>();
        status = GetComponent<IBattlePawn>().Status;
    }

    // Update is called once per frame
    void Update()
    {

    }
}