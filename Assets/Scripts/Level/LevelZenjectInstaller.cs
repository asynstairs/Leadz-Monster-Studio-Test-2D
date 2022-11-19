using LevelSignals;
using UnityEngine;
using Zenject;

public class LevelZenjectInstaller : MonoInstaller
{
    [SerializeField] private GameObject _playerPrefab;
    
    public override void InstallBindings()
    {
        DeclareSignals();
        
        Container.Bind<IGamemode>().To<GamemodeCollisionDeath>().FromNew().AsTransient().NonLazy();
        Container.Bind<TagPlayer>().FromComponentInNewPrefab(_playerPrefab).AsSingle().NonLazy();
        Container.Bind<RespawnPlayerController>().FromNew().AsSingle().NonLazy();

        BindSignals();
    }

    private void DeclareSignals()
    {
        SignalBusInstaller.Install(Container);
        Container.DeclareSignal<SignalDifficultyChanged>();
        Container.DeclareSignal<SignalGameEnded>();
        Container.DeclareSignal<SignalGameRestarted>();
        Container.DeclareSignal<SignalNeedDifficultySelect>();
        Container.DeclareSignal<SignalNeedRestart>();
        Container.DeclareSignal<SignalPlayerCollidedObstacle>();
        Container.DeclareSignal<ISignalPlayerCollided>();
    }

    private void BindSignals()
    {
        Container.BindSignal<SignalDifficultyChanged>()
            .ToMethod<PlayerMovementController>(a => a.OnDifficultySelectionChanged)
            .FromResolve();
        
        Container.BindSignal<SignalGameRestarted>()
            .ToMethod<RespawnPlayerController>(a => a.OnGameRestart)
            .FromResolve();
        
        Container.BindSignal<SignalGameEnded>()
            .ToMethod<RestartWindow>(a => a.OnGameEnded)
            .FromResolve();
        
        Container.BindSignal<SignalGameRestarted>()
            .ToMethod<ChunkPooler>(a => a.OnGameRestarted)
            .FromResolve();
        
        Container.BindSignal<SignalNeedDifficultySelect>()
            .ToMethod<DifficultiesSelectionWindow>(a => a.OnNeedDifficultySelect)
            .FromResolve();
        
        Container.BindSignal<SignalNeedRestart>()
            .ToMethod<LevelController>(a => a.OnNeedRestart)
            .FromResolve();
        
        Container.BindSignal<SignalPlayerCollidedObstacle>()
            .ToMethod<GamemodeCollisionDeath>(a => a.OnPlayerCollidedObstacle)
            .From(a => a.AsTransient());
        
        Container.BindSignal<SignalGameEnded>()
            .ToMethod<LevelController>(a => a.OnGameEnded)
            .FromResolve();
        
        Container.BindSignal<SignalGameRestarted>()
            .ToMethod<RandomTilePooler>(a => a.OnGameRestarted)
            .FromResolve();
    }
}
