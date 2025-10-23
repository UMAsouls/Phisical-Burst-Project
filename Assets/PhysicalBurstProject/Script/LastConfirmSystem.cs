using Cysharp.Threading.Tasks;
using System.Collections;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class LastConfirmSystem : ConfirmCancelCatchAble
{

    [Inject]
    IStandardUIPritner uiPritner;

    private CancellationToken token;

    protected override InputMode SelfMode => InputMode.LastConfirm;

    public async UniTask<bool> ConfirmWait()
    {
        InputModeChangeToSelf();
        uiPritner.PrintUI("Confirm");

        isCancel = false;
        isConfirm = false;

        await UniTask.WaitUntil(() => isConfirm | isCancel, cancellationToken: token);

        uiPritner.DestroyUI("Confirm");

        if(isCancel) return false;
        
        return true;
    }

    public override void Start()
    {
        token = this.GetCancellationTokenOnDestroy();

        base.Start();
    }
}