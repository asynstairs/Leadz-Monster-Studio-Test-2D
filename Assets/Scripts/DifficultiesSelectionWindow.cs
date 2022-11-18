using System;
using UniRx;
using UnityEngine;
using Zenject;

/// <summary>
/// A Window which is responsible for difficulties window view and event handling.
/// </summary>
[DisallowMultipleComponent]
public class DifficultiesSelectionWindow : MonoBehaviour, IWindow
{
    public event Action<DifficultySciptableObject> DifficultyChanged;
    
    [SerializeField] private GameObject _windowHolder;
    [SerializeField] private DifficultySelectorButton[] _difficultyButtons;
    [SerializeField] private bool _openOnStart;

    [Inject] private RestartWindow _restartWindow;
    [Inject] private StartWindow _startWindow;
    
    private readonly CompositeDisposable _disposables = new ();

    private void OnEnable()
    {
        SubscribeToObservables();
        SubscribeToEvents();
    }

    private void OnDisable()
    {
        _disposables.Dispose();
        UnsubscribeFromEvents();
    }

    private void SubscribeToEvents()
    {
        _restartWindow.SelectDifficulty += OnPlayerSelectDifficulty;
        _startWindow.SelectDifficulty += OnPlayerSelectDifficulty;
    }

    private void UnsubscribeFromEvents()
    {
        _restartWindow.SelectDifficulty -= OnPlayerSelectDifficulty;
        _startWindow.SelectDifficulty -= OnPlayerSelectDifficulty;
    }

    private void SubscribeToObservables()
    {
        SubscribeToButtons();
    }

    private void Awake()
    {
        SetOpen(_openOnStart);
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
        DifficultyChanged?.Invoke(description);
        SetOpen(false);
    }

    private void OnPlayerSelectDifficulty()
    {
        SetOpen(true);
    }

    public void SetOpen(bool open)
    {
        _windowHolder.SetActive(open);
    }
}