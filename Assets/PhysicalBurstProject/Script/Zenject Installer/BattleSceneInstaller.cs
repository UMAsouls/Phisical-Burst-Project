using UnityEngine;
using Zenject;

public class BattleSceneInstaller : MonoInstaller
{
    [SerializeField]
    GameObject gameManager;

    public override void InstallBindings()
    {
        Container
            .BindInterfacesTo<UIPrinter>()
            .AsSingle();


        Container
            .BindInterfacesTo<BattlePawnStrage>()
            .AsSingle();

        Container
            .Bind<CmdConfirmAble>()
            .To<BattleSystem>()
            .FromComponentOn(gameManager)
            .AsTransient();
    }
}