using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UniRx;
using UnityEngine;
using Zenject;

public abstract class UIProviderFeatureBase<T> : MonoBehaviour
    where T: class, IFeature
{
    [Inject] private IGamemode _gamemode;

    [CanBeNull] protected T _feature = default;
    protected readonly CompositeDisposable _disposable = new();

    protected virtual void OnEnable()
    {
        _gamemode?.Features
            .ObserveEveryValueChanged(a => a.Value)
            .Where(v => v is not null)
            .Subscribe(OnGamemodeFeaturesUpdated)
            .AddTo(_disposable);
        
        _gamemode?.FeaturesOnUpdate
            .ObserveEveryValueChanged(a => a.Value)
            .Where(v => v is not null)
            .Subscribe(OnGamemodeFeaturesUpdated)
            .AddTo(_disposable);
    }

    protected virtual void OnGamemodeFeaturesUpdated(List<IFeatureOnUpdate> features)
    {
        _feature = features.FirstOrDefault(f => f is T) as T;
        SubscribeToObservables();
    }
    
    protected virtual void OnGamemodeFeaturesUpdated(List<IFeatureOneshot> features)
    {
        _feature = features.FirstOrDefault(f => f is T) as T;
        SubscribeToObservables();
    }
    
    protected virtual void OnDisable()
    {
        _disposable.Dispose();
    }

    protected abstract void SubscribeToObservables();
}