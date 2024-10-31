using System.ComponentModel;
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
    GameObject controlableCamera;

    [SerializeField]
    GameObject pawnSelector;

    [SerializeField]
    GameObject commandStrage;

    [SerializeField]
    GameObject soundManager;

    [SerializeField]
    GameObject sePlayer;

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
            .BindInterfacesTo<ActionSlotPrinter>()
            .FromComponentOn(battleUI)
            .AsTransient();

        Container
            .BindInterfacesTo<BattleCmdSlotUIPrinter>()
            .FromComponentOn(battleUI)
            .AsTransient();

        Container
            .BindInterfacesTo<StandardUIPrinter>()
            .FromComponentOn(battleUI)
            .AsTransient();

        Container
            .BindInterfacesAndSelfTo<DamageHealUIPrinter>()
            .FromComponentOn(battleUI)
            .AsTransient();

        Container
            .BindInterfacesAndSelfTo<MiniStatusPrinter>()
            .FromComponentOn(battleUI)
            .AsTransient();

        Container
            .BindInterfacesAndSelfTo<CmdInfoUIPrinter>()
            .FromComponentOn(battleUI)
            .AsTransient();

        Container
            .BindInterfacesTo<BattlePawnStrage>()
            .AsSingle();

        Container
            .BindInterfacesAndSelfTo<ActionMaker>()
            .AsSingle();

        Container
            .BindInterfacesAndSelfTo<BattleJudge>()
            .AsSingle();

        Container
            .BindInterfacesTo<MovePosSelectSystem>()
            .FromComponentOn(gameManager)
            .AsTransient();

        Container
            .BindInterfacesTo<BattleActionSelectSystem>()
            .FromComponentOn(gameManager)
            .AsTransient();

        Container
            .BindInterfacesTo<CmdSelectSystem>()
            .FromComponentOn(gameManager)
            .AsTransient();

        Container
            .BindInterfacesTo<BattleCmdSelectSystem>()
            .FromComponentOn(gameManager)
            .AsTransient();

        Container
            .BindInterfacesTo<BattleCmdActionSelectSystem>()
            .FromComponentOn(gameManager)
            .AsTransient();

        Container
            .BindInterfacesAndSelfTo<AmbushSelectSystem>()
            .FromComponentOn(gameManager)
            .AsTransient();

        Container
            .BindInterfacesAndSelfTo<LastConfirmSystem>()
            .FromComponentOn(gameManager)
            .AsTransient();

        Container
            .BindInterfacesAndSelfTo<BattleActionUnit>()
            .FromComponentOn(gameManager)
            .AsTransient();

        Container
            .BindInterfacesAndSelfTo<MoveActionUnit>()
            .FromComponentOn(gameManager)
            .AsTransient();

        Container
            .BindInterfacesAndSelfTo<AmbushUnit>()
            .FromComponentOn(gameManager)
            .AsTransient();


        Container
            .BindInterfacesTo<PawnSelector>()
            .FromComponentOn(pawnSelector)
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
            .BindInterfacesAndSelfTo<SpellBehaviourMaker>()
            .FromComponentOn(commandMaker)
            .AsTransient();

        Container
            .BindInterfacesTo<CameraManager>()
            .FromComponentOn(cameraManager)
            .AsTransient();

        Container
            .BindInterfacesTo<ControlableCamera>()
            .FromComponentOn(controlableCamera)
            .AsTransient();

        Container
            .Bind<ICommandStrage>()
            .To<CommandStrage>()
            .FromComponentOn(commandStrage)
            .AsTransient();

        Container
            .Bind<SystemSEPlayable>()
            .To<SoundManager>()
            .FromComponentOn(soundManager)
            .AsTransient();

        Container
            .Bind<SEPlayable>()
            .To<SEPlayer>()
            .FromComponentOn(sePlayer)
            .AsTransient();

        Container
            .Bind<BGMPlayable>()
            .To<SoundManager>()
            .FromComponentOn(soundManager)
            .AsTransient();
    }
}