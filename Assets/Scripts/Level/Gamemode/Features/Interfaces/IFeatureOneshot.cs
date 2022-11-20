/// <summary>
/// May be executed any count of times per a frame.
/// </summary>
public interface IFeatureOneshot : IFeature
{
    public void Execute();
    public void Reset();
}