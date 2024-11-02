using UnityEngine;
using Zenject;

public class TipsInstaller : MonoInstaller
{
    [SerializeField]
    GameObject soundManger;

    [SerializeField]
    GameObject seplayer;

    public override void InstallBindings()
    {
        Container
            .BindInterfacesAndSelfTo<SoundManager>()
            .FromComponentOn(soundManger)
            .AsTransient();

        Container
            .BindInterfacesAndSelfTo<SEPlayer>()
            .FromComponentOn(seplayer)
            .AsTransient();

    }
}
