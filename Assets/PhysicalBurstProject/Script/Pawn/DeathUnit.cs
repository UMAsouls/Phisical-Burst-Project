using Cysharp.Threading.Tasks;
using PlasticPipe.Tube;
using System.Collections;
using UnityEngine;
using Zenject;

public class DeathUnit : MonoBehaviour,  IObserver<StatusFrag>
{
    [Inject]
    IPawnGettable strage;

    IMiniStatusController controller;
    IPawnBattleInfo info;

    public void OnComplete()
    {
        throw new System.NotImplementedException();
    }

    public async void OnNext(StatusFrag value)
    {
        await UniTask.Delay(1000, cancellationToken: destroyCancellationToken);
        controller.MiniStatusDestroy();
        Destroy(gameObject);
        strage.RemovePawn(info.ID);
    }

    // Use this for initialization
    void Start()
    {
        controller = GetComponent<IMiniStatusController>();
        info = GetComponent<IPawnBattleInfo>();

        var status = GetComponent<IBattlePawn>().Status;
        status.Subscribe(this);
    }

    // Update is called once per frame
    void Update()
    {

    }
}