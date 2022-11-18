using System;
using System.Collections.Generic;
using UniRx;

/// <summary>
/// A Gamemode is a type of rules for a level.
/// </summary>
public interface IGamemode
{
    public ReactiveProperty<List<IFeatureOneshot>> Features { get; set; }
    public ReactiveProperty<List<IFeatureOnUpdate>> FeaturesOnUpdate { get; set; }
    public event Action GameEnded;
}
