using Cysharp.Threading.Tasks;
using System.Collections;
using System.Threading;
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

    [SerializeField]
    GameObject SencerObj;

    public async UniTask Ambush(PawnActInterface pawn, float range, CancellationToken token)
    {
        var obj = Instantiate(SencerObj, pawn.Position, Quaternion.identity);
        var pawnSencer = obj.GetComponent<IPawnSencer>();
        pawnSencer.Range = range;
        
        switch(pawn.Type)
        {
            case PawnType.Enemy:
                pawnSencer.SenceType = PawnType.Member; break;
            case PawnType.Member:
                pawnSencer.SenceType = PawnType.Enemy; break;
        }

        cameraChanger.ChangeToPawnCamera(pawn.ID);
        await pawn.AmbushEffect();

        await UniTask.WaitUntil(() => pawnSencer.Senced, cancellationToken: token);
        pawnSencer.SencedTarget.ActionStop = true;
        pawnSencer.SencedTarget.GetAmbushed = true;

        IBattleCommand[] cmds = await selectSystem.Select(pawn.ID);

        await battleActionUnit.Battle(cmds, pawnSencer.SencedTarget, pawn);
        cameraChanger.ChangeToPawnCamera(pawnSencer.SencedTarget.ID);

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