using Cysharp.Threading.Tasks;
using System.Collections;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

public class AmbushUnit : MonoBehaviour
{

    [Inject]
    IBattleCmdSelectSystem selectSystem;

    [Inject]
    BattleActionUnit battleActionUnit;

    [Inject]
    CameraChangeAble cameraChanger;

    [Inject]
    DiContainer container;

    [SerializeField]
    GameObject SencerObj;

    public async UniTask Ambush(PawnActInterface pawn, float range, CancellationToken token)
    {
        var obj = container.InstantiatePrefab(SencerObj);
        obj.transform.position = pawn.Position;
        var pawnSencer = obj.GetComponent<IPawnSencer>();
        pawnSencer.Range = range;
        
        switch(pawn.Type)
        {
            case PawnType.Enemy:
                pawnSencer.SenceType = PawnType.Member; break;
            case PawnType.Member:
                pawnSencer.SenceType = PawnType.Enemy; break;
        }

        await UniTask.WaitUntil(() => pawnSencer.Senced, cancellationToken: token);
        pawnSencer.SencedTarget.ActionStop = true;
        pawnSencer.SencedTarget.GetAmbushed = true;

        cameraChanger.ChangeToPawnCamera(pawn.ID);
        await pawn.AmbushEffect();

        IBattleCommand[] cmds = await pawn.AmbushSelect(pawnSencer.SencedTarget);

        await battleActionUnit.Battle(cmds, pawnSencer.SencedTarget, pawn);
        cameraChanger.ChangeToPawnCamera(pawnSencer.SencedTarget.ID);

        pawnSencer.SencedTarget.GetAmbushed = false;

        Destroy(obj);
    }



    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}