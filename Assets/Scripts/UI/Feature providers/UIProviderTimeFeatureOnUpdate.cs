using System;
using LevelSignals;
using TMPro;
using UniRx;
using UnityEngine;
using Zenject;

public class UIProviderTimeFeatureOnUpdate : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private string _textBeforeResult;

    private readonly CompositeDisposable _disposable = new();

    [Inject]
    private void SubscribeToObservables(TimeFeatureOnUpdate timeFeatureOnUpdate)
    {
        timeFeatureOnUpdate.Result
            .ObserveEveryValueChanged(v => v.Value)
            .Subscribe(OnTimeChanged)
            .AddTo(_disposable);
    }

    private void OnTimeChanged(float time)
    {
        _text.text = $"{_textBeforeResult} {time} seconds.";
    }

    private void OnDestroy()
    {
        _disposable.Dispose();
    }
}
