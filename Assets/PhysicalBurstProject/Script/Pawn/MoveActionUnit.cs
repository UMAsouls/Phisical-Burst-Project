using Cysharp.Threading.Tasks;
using System.Collections;
using System.Threading;
using UnityEngine;

public class MoveActionUnit : MonoBehaviour, IMoveActionUnit
{

    [SerializeField]
    private float MoveSpeed;

    private bool isBattle;

    public bool IsBattle { set => isBattle = value; }

    private bool isComplete;

    private Vector2 destination;

    private CancellationToken cts;

    public async UniTask Move(Vector2 delta, IEmergencyBattleUnit pawn)
    {
        isBattle = false;
        isComplete = false;

        Vector2 dir = delta.normalized;

        destination = (Vector2)transform.position + dir;

        await UniTask.WaitUntil(() => isBattle || isComplete, cancellationToken: cts);

        if (isBattle) await pawn.EmergencyBattle();
    }

    public async UniTask DoMove(Vector2 dir)
    {
        while (!isBattle || !isComplete)
        {
            float dt = Time.deltaTime;
            float delta = dt*MoveSpeed;

            if (Vector2.Distance(transform.position, destination) < delta)
            {
                transform.position = destination;
                isComplete = true;
            }
            else transform.position += (Vector3)(delta * dir);

            await UniTask.Yield(PlayerLoopTiming.Update, cancellationToken: cts);
        }
    }

    // Use this for initialization
    void Start()
    {
        cts = this.GetCancellationTokenOnDestroy();
    }

    // Update is called once per frame
    void Update()
    {

    }
}