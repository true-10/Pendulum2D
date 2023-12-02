using Pendulum2D;
using UnityEngine;
using Zenject;

public class ProjectInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<PlayerData>().AsSingle();
    }
}