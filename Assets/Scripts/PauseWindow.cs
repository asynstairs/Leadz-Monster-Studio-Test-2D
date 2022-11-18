using System;
using UnityEngine;
using UnityEngine.UI;

public class PauseWindow : MonoBehaviour, IWindow
{
    public event Action Play;
    public event Action Resume;
    public event Action Restart;
    public event Action Pause;
    public event Action SelectDifficulty;

    [SerializeField] private GameObject _holder;
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _pauseButton;
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _selectDifficultyButton;

    private void Start()
    {
        SetOpen(true);
    }

    private void OnEnable()
    {
        _playButton.onClick.AddListener(OnPlayButtonPressed);
        _selectDifficultyButton.onClick.AddListener(OnSelectDifficultyButtonPressed);
        _pauseButton.onClick.AddListener(OnPauseButtonPressed);
    }

    private void OnPauseButtonPressed()
        => Pause?.Invoke();

    private void OnSelectDifficultyButtonPressed()
        => SelectDifficulty?.Invoke();

    private void OnPlayButtonPressed()
        => Play?.Invoke();

    public void SetOpen(bool open)
    {
        _holder.SetActive(open);
    }
}
