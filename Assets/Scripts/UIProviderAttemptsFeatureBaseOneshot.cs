using TMPro;
using UniRx;
using UnityEngine;

public class UIProviderAttemptsFeatureBaseOneshot : UIProviderFeatureBase<AttemptsFeatureOneshot>
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private string _textBeforeResult;

    protected override void SubscribeToObservables()
    {
        _feature?.Result
            .ObserveEveryValueChanged(v => v.Value)
            .Subscribe(OnCountAttemptsChanged)
            .AddTo(_disposable);
    }

    private void OnCountAttemptsChanged(uint count)
    {
        _text.text = $"Attempts: {count}";
    }
}