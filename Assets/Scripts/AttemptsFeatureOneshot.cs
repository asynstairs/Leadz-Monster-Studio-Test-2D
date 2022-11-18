using UnityEngine;

/// <summary>
/// Counts attempts.
/// </summary>
public class AttemptsFeatureOneshot : FeatureBase<uint>, IFeatureOneshot
{
    public void Execute()
    {
        Result.Value++;
    }
}