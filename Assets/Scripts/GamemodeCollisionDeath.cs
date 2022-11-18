using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Zenject;

/// <summary>
/// Listens to the player's collider collision events.
/// If the collider collides with a TagObstacle, the game ends.
/// </summary>
public class GamemodeCollisionDeath: IInitializable, IDisposable, IGamemode
{
    public ReactiveProperty<List<IFeatureOneshot>> Features { get; set; } = new();
    public ReactiveProperty<List<IFeatureOnUpdate>> FeaturesOnUpdate { get; set; } = new();
 
    public event Action GameEnded;
    
    [Inject] private TagPlayer _player;
    
    private CollisionEventBroadcaster _playerCollisionBroadcaster;
    private readonly CompositeDisposable _disposable = new();

    [Inject]
    public void Initialize()
    {
        _playerCollisionBroadcaster = _player.gameObject.GetComponent<CollisionEventBroadcaster>();
        SubscribeToPlayerCollision();
    }

    public void Dispose()
    {
        _disposable?.Dispose();
    }
    
    private void SubscribeToPlayerCollision()
    {
        if (_playerCollisionBroadcaster is not null)
        {
            _playerCollisionBroadcaster.CollisionEnter += OnPlayerCollisionEnter;
        }
    }

    private void OnPlayerCollisionEnter(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<TagObstacle>())
        {
            GameEnded?.Invoke(); 
        }
    }
}