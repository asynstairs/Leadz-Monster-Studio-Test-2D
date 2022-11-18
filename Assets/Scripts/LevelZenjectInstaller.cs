using Zenject;

public class LevelZenjectInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<IGamemode>().To<GamemodeCollisionDeath>().FromNew().AsSingle().Lazy();
    }
}
