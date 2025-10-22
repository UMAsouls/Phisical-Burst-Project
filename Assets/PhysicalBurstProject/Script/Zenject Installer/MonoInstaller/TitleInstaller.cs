using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using Zenject;

public class TitleInstaller : MonoInstaller
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