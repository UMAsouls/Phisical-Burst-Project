using Cysharp.Threading.Tasks;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public abstract class EasyEffectBehaviour<T> : CommandBehaviourBase<T> where T : IEasyEffectCommand
{

    public async UniTask PawnEffect(PawnActInterface pawn)
    {
        camerChanger.ChangeToPawnCamera(pawn.ID);
        var zoomController = camerChanger.GetZoomController();
        zoomController.OrthoSize = pawn.Size;
        sePlayer.PlaySE(cmd.PawnEffectSound);
        await cmd.PawnEffect(pawn.Position, pawn.Size-1);
    }

    public async UniTask MainEffect(Vector2 pos)
    {
        cameraController.Position = pos;
        camerChanger.ChangeToMovableCamera();
        var zoomController = camerChanger.GetZoomController();
        zoomController.ZoomSpeed = Mathf.Max(3f, cmd.EffectScale*0.7f) ;
        zoomController.OrthoSize = cmd.EffectScale + 0.5f;
        await UniTask.Delay(300);
        sePlayer.PlaySE(cmd.AttackEffectSound);
        await cmd.AttackEffect(pos);
    }
}
