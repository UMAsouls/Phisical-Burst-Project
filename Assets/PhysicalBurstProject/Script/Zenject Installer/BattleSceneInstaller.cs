using UnityEngine;
using Zenject;

public class BattleSceneInstaller : MonoInstaller
{
    [SerializeField]
    GameObject gameManager;

    [SerializeField]
    GameObject battleUI;

    [SerializeField]
    GameObject commandMaker;

    [SerializeField]
    GameObject cameraManager;

    [SerializeField]
    GameObject selectPhazeCamera;

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

        Container
            .BindInterfacesTo<CmdSelectSystem>()
            .FromComponentOn(gameManager)
            .AsTransient();


        Container
            .BindInterfacesTo<CommandBehaviourMaker>()
            .FromComponentOn(commandMaker)
            .AsTransient();

        Container
            .BindInterfacesAndSelfTo<HealMaker>()
            .FromComponentOn(commandMaker)
            .AsTransient();

        Container
            .BindInterfacesAndSelfTo<LongRangeMaker>()
            .FromComponentOn(commandMaker)
            .AsTransient();

        Container
            .BindInterfacesAndSelfTo<RangeAttackMaker>()
            .FromComponentOn(commandMaker)
            .AsTransient();

        Container
            .BindInterfacesTo<CameraManager>()
            .FromComponentOn(cameraManager)
            .AsTransient();

        Container
            .BindInterfacesTo<SelectPhazeCamera>()
            .FromComponentOn(selectPhazeCamera)
            .AsTransient();
    }
}