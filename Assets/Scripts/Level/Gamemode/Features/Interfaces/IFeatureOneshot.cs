/// <summary>
/// A Feature is a micro functionality for gamemodes.
/// </summary>
public interface IFeatureOneshot : IFeature
{
    public void Execute();
    public void Reset();
}