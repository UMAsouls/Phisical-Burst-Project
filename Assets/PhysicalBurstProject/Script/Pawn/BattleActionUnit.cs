using Cysharp.Threading.Tasks;
using System.Collections;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class BattleActionUnit : MonoBehaviour
{
    [Inject]
    IBattleCmdSlotUIPrinter uiPrinter;

    SlotWindowControlable selfUIController;
    SlotWindowControlable targetUIController;

    [Inject]
    BattleJudge judge;

    private bool isBurst;

    private bool countComplete = true;

    private PlayerInput input;


    public void OnBurst(InputAction.CallbackContext context)
    {
        if(context.performed) isBurst = true;
    }

    private async UniTask Count(float second, CancellationToken token)
    {
        countComplete = false;
        while(second > 0 && !token.IsCancellationRequested)
        {
            await UniTask.Yield(PlayerLoopTiming.Update, token);
            second -= Time.deltaTime;
        }
        countComplete = true;
    }

    public async UniTask Battle(IBattleCommand[] cmds, AttackAble target, PawnActInterface pawn)
    {

        await target.EmergencyBattle();

        var dir1 = (target.Position - pawn.Position);
        dir1 = new Vector2(-1 * dir1.x, dir1.y);
        var dir2 = (pawn.Position - target.Position);
        dir2 = new Vector2(-1 * dir2.x, dir2.y);
        selfUIController = uiPrinter.PrintUIAtWorld(pawn.Position, dir1);
        targetUIController = uiPrinter.PrintUIAtWorld(target.Position, dir2);

        var enemyCmds = target.EmergencyCmds;

        for (int i = 0; i < cmds.Length; i++)
        {
            selfUIController.ActionSet(cmds[i].Name, i);
            targetUIController.ActionSet(enemyCmds[i].Name, i);
        }

        for (int i = 0; i < cmds.Length; i++)
        {
            
            pawn.FightStart();
            target.FightStart();

            var cmd1 = cmds[i];
            var cmd2 = enemyCmds[i];

            var j1 = judge.Judge(cmd1.Type, cmd2.Type, pawn.Priority - target.Priority);
            var j2 = judge.Judge(cmd2.Type, cmd1.Type, target.Priority - pawn.Priority);

            if (!j1) selfUIController.YellowFocusAnim(i);
            else selfUIController.BlueFocusAnim(i);

            if (!j2) targetUIController.YellowFocusAnim(i);
            else targetUIController.BlueFocusAnim(i);


            CancellationTokenSource cts = new CancellationTokenSource();
            bool enBurst;
            do
            {
                isBurst = false;
                enBurst = false;
                if (countComplete)
                {
                    cts = new CancellationTokenSource();
                    Count(1f, cts.Token).Forget();
                }

                input.SwitchCurrentActionMap("Fight");
                await UniTask.WaitUntil(() => isBurst || countComplete, cancellationToken: destroyCancellationToken);
                input.SwitchCurrentActionMap("None");

                if (target.Type == PawnType.Enemy)
                {
                    enBurst = BurstJudge(j2);
                    if (isBurst && !pawn.Burst) { pawn.PhysicalBurst(); cts.Cancel(); countComplete = true; }
                    else if (enBurst && !target.Burst) { target.PhysicalBurst(); cts.Cancel(); countComplete = true; }
                    else  { isBurst = false; enBurst = false; }
                }else
                {
                    enBurst = BurstJudge(j1);
                    if (isBurst && !target.Burst) { target.PhysicalBurst(); cts.Cancel(); countComplete = true; }
                    else if (enBurst && !pawn.Burst) { pawn.PhysicalBurst(); cts.Cancel(); countComplete = true; }
                    else { isBurst = false; enBurst = false; }
                }

            } while (isBurst  || enBurst || !countComplete);

            if (pawn.IsStun) pawn.FightEnd();
            else cmd1.Do(pawn, target, cmd2.Type).Forget();
            if (target.IsStun) target.FightEnd();
            else cmd2.Do(target, pawn, cmd1.Type).Forget();

            await UniTask.WaitUntil(() => pawn.AttackEnd && target.AttackEnd, cancellationToken: destroyCancellationToken);

            pawn.FightEnd();
            target.FightEnd();

            selfUIController.AnimEnd(i);
            targetUIController.AnimEnd(i);

            await UniTask.Delay(500);
        }

        uiPrinter.DestroyUI();
        uiPrinter.DestroyUI();
    }

    private bool BurstJudge(bool judge)
    {
        if (!judge) return Random.Range(1, 100) <= 90;
        else return Random.Range(1, 100) <= 10;
    }


    // Use this for initialization
    void Start()
    {
        input = GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}