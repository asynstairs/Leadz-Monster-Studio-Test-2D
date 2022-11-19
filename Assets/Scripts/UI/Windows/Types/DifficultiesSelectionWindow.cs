using LevelSignals;
using UniRx;
using UnityEngine;
using Zenject;

/// <summary>
/// A Window which is responsible for difficulties window view and event handling.
/// </summary>
[DisallowMultipleComponent]
public class DifficultiesSelectionWindow : MonoBehaviour, IWindow
{
    [SerializeField] private GameObject _windowHolder;
    [SerializeField] private DifficultySelectorButton[] _difficultyButtons;
    [SerializeField] private bool _openOnStart;
    [Inject] private readonly SignalBus _signalBus = null;
    private readonly CompositeDisposable _disposables = new ();

    private void OnEnable()
    {
        SubscribeToObservables();
    }

    private void OnDisable()
    {
        _disposables.Dispose();
    }

    private void Start()
    {
        SetOpen(_openOnStart);
    }
    
    private void SubscribeToObservables()
    {
        SubscribeToButtons();
    }

    private void SubscribeToButtons()
    {
        foreach (var button in _difficultyButtons)
        {
            button.Clicked += ButtonOnClicked;
        }
    }

    private void ButtonOnClicked(DifficultySciptableObject description)
    {
        _signalBus.Fire<SignalDifficultyChanged>(new(){Difficulty = description});
        SetOpen(false);
    }

    public void OnNeedDifficultySelect(SignalNeedDifficultySelect signalNeedDifficultySelect)
    {
        SetOpen(true);
    }
    
    public void SetOpen(bool open)
    {
        _windowHolder.SetActive(open);
    }
}