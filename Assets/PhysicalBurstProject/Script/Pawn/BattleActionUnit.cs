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

    private bool countComplete;


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

    public async UniTask Battle(IBattleCommand[] cmds, AttackAble battlePawn, PawnActInterface pawn)
    {
        await battlePawn.EmergencyBattle();

        selfUIController = uiPrinter.PrintUILeft();
        targetUIController = uiPrinter.PrintUIRight();

        var enemyCmds = battlePawn.EmergencyCmds;

        for (int i = 0; i < cmds.Length; i++)
        {
            selfUIController.ActionSet(cmds[i].Name, i);
            targetUIController.ActionSet(enemyCmds[i].Name, i);
        }

        for (int i = 0; i < cmds.Length; i++)
        {
            

            var cmd1 = cmds[i];
            var cmd2 = enemyCmds[i];

            var j1 = judge.Judge(cmd1.Type, cmd2.Type, pawn.Priority - battlePawn.Priority);
            var j2 = judge.Judge(cmd2.Type, cmd1.Type, battlePawn.Priority - pawn.Priority);

            if (!j1) selfUIController.YellowFocusAnim(i);
            else selfUIController.BlueFocusAnim(i);

            if (!j2) targetUIController.YellowFocusAnim(i);
            else targetUIController.BlueFocusAnim(i);

            do
            {
                isBurst = false;
                CancellationTokenSource cts = new CancellationTokenSource();
                Count(1, cts.Token).Forget();

                await UniTask.WaitUntil(() => isBurst || countComplete, cancellationToken: destroyCancellationToken);

                cts.Cancel();
                
                if (battlePawn.Type == PawnType.Enemy)
                {
                    var enBurst = BurstJudge(j2);
                    if (isBurst && !pawn.Burst) pawn.PhysicalBurst();
                    else if(enBurst && !battlePawn.Burst) battlePawn.PhysicalBurst();
                    else isBurst = false;
                }else
                {
                    var enBurst = BurstJudge(j1);
                    if (isBurst && !battlePawn.Burst) battlePawn.PhysicalBurst();
                    else if (enBurst && !pawn.Burst) pawn.PhysicalBurst();
                    else isBurst = false;
                }
            } while (isBurst);
            
        }
    }

    private bool BurstJudge(bool judge)
    {
        if (judge) return Random.Range(1, 100) <= 90;
        else return Random.Range(1, 100) <= 10;
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