using Cysharp.Threading.Tasks;
using System.Collections;
using UnityEngine;
using Zenject;

public class ActionUnit : MonoBehaviour, IActionUnit
{
    [Inject]
    private AmbushUnit ambushUnit;

    [Inject]
    private BattleActionUnit battleActionUnit;

    [Inject]
    private MoveActionUnit moveActionUnit;


    public async UniTask Action(IActionCommandBehaviour action)
    {
        await action.DoAction();
    }

    public async UniTask Ambush(float range)
    {
        ambushUnit.Ambush(, range).Forget();
    }

    public async UniTask Battle(IBattleCommand[] cmds, AttackAble target, int priorityBonus)
    {
        //pawn.Priority += PriorityBonus;
        await battleActionUnit.Battle(cmds, target);
    }

    public async UniTask MovePos(Vector2 delta)
    {
        await moveActionUnit.DoMove(delta, destroyCancellationToken);
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