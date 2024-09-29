using UnityEngine;
using Zenject;

public class BattleSceneInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container
            .BindInterfacesTo<UIPrinter>()
            .AsSingle();

        Container
            .BindInterfacesTo<BattleSystem>()
            .AsSingle();


        Container
            .BindInterfacesTo<BattlePawnStrage>()
            .AsSingle();
        
    }
}