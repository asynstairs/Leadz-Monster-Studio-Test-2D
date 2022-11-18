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

    [Inject] private PauseWindow _pauseWindow;
    
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
        _pauseWindow.SelectDifficulty += OnSelectDifficulty;
    }

    private void UnsubscribeFromEvents()
    {
        _pauseWindow.Pause -= OnSelectDifficulty;
    }

    private void SubscribeToObservables()
    {
        SubscribeToButtons();
    }

    private void Awake()
    {
        SetOpen(false);
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

    private void OnSelectDifficulty()
    {
        SetOpen(true);
    }

    public void SetOpen(bool open)
    {
        _windowHolder.SetActive(open);
    }
}