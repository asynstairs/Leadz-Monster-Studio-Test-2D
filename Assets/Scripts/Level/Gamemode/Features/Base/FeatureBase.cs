using UniRx;

/// <summary>
/// A feature is a micro-functionality for a gamemode.
/// It calculates some Result to which other subscribe.
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class FeatureBase<T>
{
    public readonly ReactiveProperty<T> Result = new();

    public virtual void Reset()
    {
        Result.Value = default;
    }
}