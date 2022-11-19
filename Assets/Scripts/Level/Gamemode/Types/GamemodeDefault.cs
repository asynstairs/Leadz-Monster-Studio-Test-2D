using System.Collections.Generic;
using LevelSignals;
using UniRx;
using Zenject;

/// <summary>
/// Type of IGamemode which ends if 
/// </summary>
public class GamemodeDefault : IGamemode
{
    public ReactiveProperty<List<IFeatureOneshot>> Features { get; set; } = new();
    public ReactiveProperty<List<IFeatureOnUpdate>> FeaturesOnUpdate { get; set; } = new();
    
    [Inject] private SignalBus _signalBus;
    
    public void OnGamemodeTriggered(ISignalGamemodeTriggered signalGamemodeTriggered)
    {
        _signalBus.Fire<SignalGameEnded>();
    }
}