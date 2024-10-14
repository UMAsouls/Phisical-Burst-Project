using UnityEngine;
using Zenject;

public class BattleSceneInstaller : MonoInstaller
{
    [SerializeField]
    GameObject gameManager;

    [SerializeField]
    GameObject battleUI;

    public override void InstallBindings()
    {
        Container
            .BindInterfacesTo<BattleUIPrinter>()
            .FromComponentOn(battleUI)
            .AsTransient();

        Container
            .BindInterfacesTo<PosSelectorUIPrinter>()
            .FromComponentOn(battleUI)
            .AsTransient();


        Container
            .BindInterfacesTo<BattlePawnStrage>()
            .AsSingle();

        Container
            .BindInterfacesTo<ActionMaker>()
            .AsSingle();

        Container
            .BindInterfacesTo<MovePosSelectSystem>()
            .FromComponentOn(gameManager)
            .AsTransient();

        Container
            .BindInterfacesTo<BattleCmdSelectSystem>()
            .FromComponentOn(gameManager)
            .AsTransient();
    }
}