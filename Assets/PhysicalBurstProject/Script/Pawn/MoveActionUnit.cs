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
        pawn.GetAmbushed = false;
        isComplete = false;

        Vector2 dir = delta.normalized;
        Debug.Log(dir);

        destination = (Vector2)pawn.Position + delta;

        pawn.MoveAnimation(dir);

        CancellationTokenSource tokenSource = new CancellationTokenSource();
        DoMove(dir, tokenSource.Token ,pawn).Forget();

        await UniTask.WaitUntil(() => pawn.GetAmbushed || isComplete, cancellationToken: ct);

        pawn.EndMove();
        tokenSource.Cancel();

        if (pawn.GetAmbushed) pawn.IsMove = false;
    }

    public async UniTask DoMove(Vector2 dir, CancellationToken token, PawnActInterface pawn)
    {
        if(!pawn.Burst) pawn.IsMove = true;
        else pawn.IsMove = false;

        while (!pawn.GetAmbushed && !isComplete && !token.IsCancellationRequested)
        {

            float dt = Time.deltaTime;
            float dis = dt * MoveSpeed;

            if (Vector2.Distance(pawn.Position, destination) < dis)
            {
                pawn.Position = destination;
                isComplete = true;
            }
            else pawn.Position += (dis * dir);

            await UniTask.Yield(PlayerLoopTiming.Update, cancellationToken: ct);
        }
        pawn.IsMove = false;
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