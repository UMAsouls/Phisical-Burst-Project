using UnityEngine;
using Zenject;

public class AddCmdSceneInstaller : MonoInstaller
{

    [SerializeField]
    GameObject soundManager;

    [SerializeField]
    GameObject commandAdder;

    [SerializeField]
    GameObject commandStrage;

    private void Init()
    {
        var comp = commandAdder.GetComponent<CommandAdder>();
        comp.MakeInstance();
    }

    public override void InstallBindings()
    {
        Init();

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
            .Bind<BGMPlayable>()
            .To<SoundManager>()
            .FromComponentOn(soundManager)
            .AsTransient();

        Container
            .Bind(typeof(IBroker<InputModeTopic, InputMode>))
            .To<Broker<InputModeTopic, InputMode>>()
            .AsSingle();

        Container
            .Bind(typeof(IBroker<ActionSetTopic, ActionSetMessage>))
            .To<Broker<ActionSetTopic, ActionSetMessage>>()
            .AsSingle();

        Container
            .BindInterfacesAndSelfTo<CommandAdder>()
            .FromMethod((ctx) => CommandAdder.Instance)
            .AsSingle();
    }
}