using System.Collections.Generic;
using LevelSignals;
using UniRx;
using Zenject;

/// <summary>
/// Listens to the player's collider collision events.
/// If the collider collides with a TagObstacle, the game ends.
/// </summary>
public class GamemodeCollisionDeath : IGamemode
{
    public ReactiveProperty<List<IFeatureOneshot>> Features { get; set; } = new();
    public ReactiveProperty<List<IFeatureOnUpdate>> FeaturesOnUpdate { get; set; } = new();

    [Inject] private readonly SignalBus _signalBus;

    public void OnPlayerCollidedObstacle(SignalPlayerCollidedObstacle signalPlayerCollidedObstacle)
    {
        _signalBus.Fire<SignalGameEnded>();
    }
}