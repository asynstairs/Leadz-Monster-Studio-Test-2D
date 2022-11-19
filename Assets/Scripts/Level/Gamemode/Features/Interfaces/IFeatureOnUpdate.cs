/// <summary>
/// This is a Feature that needs to be executed each frame.
/// </summary>
public interface IFeatureOnUpdate : IFeature
{
    public void ExecuteOnUpdate(float deltaTime);
    public void Reset();
}