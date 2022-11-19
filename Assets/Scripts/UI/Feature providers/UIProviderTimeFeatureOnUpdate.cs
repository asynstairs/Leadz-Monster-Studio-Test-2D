using TMPro;
using UniRx;
using UnityEngine;

public class UIProviderTimeFeatureOnUpdate : UIProviderFeatureBase<TimeFeatureOnUpdate>
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private string _textBeforeResult;
    
    protected override void SubscribeToObservables()
    {
        _feature?.Result
            .ObserveEveryValueChanged(v => v.Value)
            .Subscribe(OnTimeChanged)
            .AddTo(_disposable);
    }

    private void OnTimeChanged(float time)
    {
        _text.text = $"{_textBeforeResult} {time} seconds.";
    }
}
