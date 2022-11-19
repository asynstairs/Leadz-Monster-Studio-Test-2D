using System.Collections.Generic;
using LevelSignals;
using UniRx;
using UnityEngine;
using Zenject;

public class LevelZenjectInstaller : MonoInstaller
{
    [SerializeField] private GameObject _playerPrefab;
    
    public override void InstallBindings()
    {
        DeclareSignals();
        BindSignals();
        
        // Container.Bind<IGamemode>()
        //     .To<GamemodeCollisionDeath>()
        //     .FromMethod(CreateGamemodeWithInjectedFeatures)
        //     .AsSingle()
        //     .Lazy();
        
        CreateGamemodeWithInjectedFeatures();
        
        Container.Bind<TagPlayer>()
            .FromComponentInNewPrefab(_playerPrefab)
            .AsSingle()
            .Lazy();
        
        Container.Bind<RespawnPlayerController>()
            .FromNew()
            .AsSingle()
            .NonLazy();
    }

    private void CreateGamemodeWithInjectedFeatures()
    {
        LevelData data = BinarySerializer.Deserialize();
        
        AttemptsFeatureOneshot attemptsFeatureOneshot = new();
        attemptsFeatureOneshot.Result.Value = data.Attempts;
        
        TimeFeatureOnUpdate timeFeature = new();

        Container.BindInstance(attemptsFeatureOneshot).AsSingle().Lazy();
        Container.BindInstance(timeFeature).AsSingle().Lazy();
            
        GamemodeCollisionDeath gamemode = new();

        gamemode.Features = new ReactiveProperty<List<IFeatureOneshot>>();
        
        gamemode.Features.Value = new List<IFeatureOneshot>()
        {
            attemptsFeatureOneshot
        };
        
        gamemode.FeaturesOnUpdate = new ReactiveProperty<List<IFeatureOnUpdate>>();
        
        gamemode.FeaturesOnUpdate.Value = new List<IFeatureOnUpdate>()
        {
            timeFeature
        };

        Container.Bind<IGamemode>().FromInstance(gamemode).AsSingle().NonLazy();
        
        Container.QueueForInject(gamemode);
    }

    private GamemodeCollisionDeath CreateGamemodeWithInjectedFeatures(InjectContext context)
    {
        LevelData data = BinarySerializer.Deserialize();
        
        AttemptsFeatureOneshot attemptsFeatureOneshot = new();
        attemptsFeatureOneshot.Result.Value = data.Attempts;
        
        TimeFeatureOnUpdate timeFeature = new();

        context.Container.BindInstance(attemptsFeatureOneshot).AsSingle().Lazy();
        context.Container.BindInstance(timeFeature).AsSingle().Lazy();
            
        GamemodeCollisionDeath gamemode = new();

        gamemode.Features = new ReactiveProperty<List<IFeatureOneshot>>();
        
        gamemode.Features.Value = new List<IFeatureOneshot>()
        {
            attemptsFeatureOneshot
        };
        
        gamemode.FeaturesOnUpdate = new ReactiveProperty<List<IFeatureOnUpdate>>();
        
        gamemode.FeaturesOnUpdate.Value = new List<IFeatureOnUpdate>()
        {
            timeFeature
        };

        return gamemode;
    }
    
    private AttemptsFeatureOneshot CreateAttempsFeatureOneshot()
    {
        LevelData data = BinarySerializer.Deserialize();
        AttemptsFeatureOneshot feature = new();
        feature.Result.Value = data.Attempts;
        return feature;
    }

    private void DeclareSignals()
    {
        SignalBusInstaller.Install(Container);
        
        Container.DeclareSignal<SignalDifficultyChanged>();
        Container.DeclareSignal<SignalGameEnded>();
        Container.DeclareSignal<SignalGameRestarted>();
        Container.DeclareSignal<SignalNeedDifficultySelect>();
        Container.DeclareSignal<SignalNeedRestart>();
        Container.DeclareSignal<SignalGamemodeFeaturesConstructed>();
        Container.DeclareSignal<ISignalGamemodeTriggered>();
        Container.DeclareSignal<SignalGamemodeCollisionDeathTriggered>().OptionalSubscriber();
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
        
        Container.BindSignal<ISignalGamemodeTriggered>()
            .ToMethod<IGamemode>(a => a.OnGamemodeTriggered)
            .FromResolve();
        
        Container.BindSignal<SignalGameEnded>()
            .ToMethod<LevelController>(a => a.OnGameEnded)
            .FromResolve();
        
        Container.BindSignal<SignalGameRestarted>()
            .ToMethod<RandomTilePooler>(a => a.OnGameRestarted)
            .FromResolve();
    }
}
