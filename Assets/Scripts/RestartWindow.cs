using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class RestartWindow : MonoBehaviour, IWindow
{
    public event Action Restart;
    public event Action SelectDifficulty;

    [Inject] private LevelController _levelController;

    [SerializeField] private GameObject _holder;
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _selectDifficultyButton;

    [SerializeField] private bool _openOnStart;

    private void Start()
    {
        SetOpen(_openOnStart);
    }

    private void OnEnable()
    {
        SubscribeToEvents();
    }

    private void OnDisable()
    {
        UnsubscribeFromEvents();
    }

    private void SubscribeToEvents()
    {
        _levelController.GameEnded += OnGameEnded;
        _selectDifficultyButton.onClick.AddListener(OnSelectDifficultyButtonPressed);
        _restartButton.onClick.AddListener(OnRestartButtonPressed);
    }

    private void UnsubscribeFromEvents()
    {
        _levelController.GameEnded -= OnGameEnded;
    }

    private void OnGameEnded()
    {
        
        SetOpen(true);
    }

    private void OnRestartButtonPressed()
    {
        Restart?.Invoke();
        SetOpen(false);
    }

    private void OnSelectDifficultyButtonPressed()
    {
        SelectDifficulty?.Invoke();
    }
    
    public void SetOpen(bool open)
    {
        _holder.SetActive(open);
    }
}
