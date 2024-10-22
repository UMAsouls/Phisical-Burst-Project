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

    private PlayerInput input;

    public async UniTask<bool> ConfirmWait()
    {
        input.SwitchCurrentActionMap("LastConfirm");
        uiPritner.PrintUI("Confirm");

        isCancel = false;
        isConfirm = false;

        await UniTask.WaitUntil(() => isConfirm | isCancel, cancellationToken: token);

        uiPritner.DestroyUI("Confirm");
        input.SwitchCurrentActionMap("None");

        if(isCancel) return false;
        
        return true;
    }

    private void Start()
    {
        input = GetComponent<PlayerInput>();
        token = this.GetCancellationTokenOnDestroy();
    }
}