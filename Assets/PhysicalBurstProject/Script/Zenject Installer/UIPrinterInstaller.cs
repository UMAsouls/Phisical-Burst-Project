using UnityEngine;
using Zenject;

public class UIPrinterInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container
            .BindInterfacesTo<UIPrinter>()
            .AsSingle();

        
    }
}