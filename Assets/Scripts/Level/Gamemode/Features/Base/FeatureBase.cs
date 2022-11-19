using UniRx;

public abstract class FeatureBase<T>
{
    public readonly ReactiveProperty<T> Result = new();

    public virtual void Reset()
    {
        Result.Value = default;
    }
}