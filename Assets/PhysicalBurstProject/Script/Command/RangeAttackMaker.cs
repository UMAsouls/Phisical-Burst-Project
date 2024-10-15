
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

public class RangeAttackMaker : MonoBehaviour
{

    private bool isConfirm;

    private bool isCancel;

    public void OnConfirm(InputAction.CallbackContext context)
    {
        if(context.performed) isConfirm = true;
    }

    public void OnCancel(InputAction.CallbackContext context)
    {
        if (context.performed) isCancel = true;
    }

    public async UniTask<IActionCommandBehaviour> MakeBehaviour(IRangeAttackCommand cmd)
    {
        await UniTask.WaitUntil(() => isConfirm | isCancel);
        if(isConfirm) { return new RangeAttackBehaviour(cmd);  }
        else { return null; }
    }
}
