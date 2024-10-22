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

    private CancellationToken ct;

    public async UniTask Move(Vector2 delta, PawnActInterface pawn)
    {
        isBattle = false;
        isComplete = false;

        Vector2 dir = delta.normalized;
        Debug.Log(dir);

        destination = (Vector2)transform.position + delta;

        pawn.MoveAnimation(dir);

        CancellationTokenSource tokenSource = new CancellationTokenSource();
        DoMove(dir, tokenSource.Token).Forget();

        await UniTask.WaitUntil(() => isBattle || isComplete, cancellationToken: ct);

        pawn.EndMove();
        tokenSource.Cancel();

        if (isBattle) await pawn.EmergencyBattle();
    }

    public async UniTask DoMove(Vector2 dir, CancellationToken token)
    {
        while (!isBattle && !isComplete && !token.IsCancellationRequested)
        {

            float dt = Time.deltaTime;
            float dis = dt*MoveSpeed;

            if (Vector2.Distance(transform.position, destination) < dis)
            {
                transform.position = destination;
                isComplete = true;
            }
            else transform.position += (Vector3)(dis * dir);

            await UniTask.Yield(PlayerLoopTiming.Update, cancellationToken: ct);
        }
    }

    // Use this for initialization
    void Start()
    {
        ct = this.GetCancellationTokenOnDestroy();
    }

    // Update is called once per frame
    void Update()
    {

    }
}