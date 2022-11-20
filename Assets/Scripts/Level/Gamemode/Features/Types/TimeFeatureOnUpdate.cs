/// <summary>
/// Count how much time passed.
/// </summary>
public class TimeFeatureOnUpdate : FeatureBase<float>, IFeatureOnUpdate
{
    public void ExecuteOnUpdate(float deltaTime)
    {
        Result.Value += deltaTime;
    }
}