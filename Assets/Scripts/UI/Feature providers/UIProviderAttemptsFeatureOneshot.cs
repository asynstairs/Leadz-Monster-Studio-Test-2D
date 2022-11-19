using TMPro;
using UniRx;
using UnityEngine;
using Zenject;

public class UIProviderAttemptsFeatureOneshot : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private string _textBeforeResult;
    
    private readonly CompositeDisposable _disposable = new();
    
    [Inject]
    private void SubscribeToObservable(AttemptsFeatureOneshot attemptsFeatureOneshot)
    {
        attemptsFeatureOneshot.Result
            .ObserveEveryValueChanged(f => f.Value)
            .Subscribe(OnCountAttemptsChanged)
            .AddTo(_disposable);
    }
    
    private void OnCountAttemptsChanged(uint count)
    {
        _text.text = $"{_textBeforeResult}: {count}";
    }
}