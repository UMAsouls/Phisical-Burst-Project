using Cysharp.Threading.Tasks;
using System.Collections;
using UnityEngine;
using Zenject;
using System.Threading;

public class PawnSelector : ConfirmCancelCatchAble, IPawnSelector
{
    [Inject]
    private IPosSelectorUIPrinter posSelectorUIPrinter;

    Ray ray;

    SelectedPawn pawn;

    private CancellationToken token;

    public async UniTask<int> PawnSelect(int pawnID, PawnType type)
    {
        posSelectorUIPrinter.PrintPosSelectorUI();

        await UniTask.WaitUntil(() => (isConfirm | isCancel), cancellationToken: token);

        if (isCancel || pawn == null) return -1; 

        return pawn.ID;
    }

    private void RayHit(Collider2D collider)
    {
        var obj = collider.gameObject;
        pawn = obj.GetComponent<SelectedPawn>();
        if (pawn == null) return;

        pawn.SelectedFocus();
    }



    // Use this for initialization
    void Start()
    {
        token = this.GetCancellationTokenOnDestroy();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 screenCenter = new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0.0f);
        RaycastHit2D hit = Physics2D.Raycast(screenCenter, Vector2.zero);

        if (hit.collider != null) RayHit(hit.collider);
        else pawn = null;
    }
}