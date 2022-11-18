using System;
using UnityEngine;
using UnityEngine.UI;

public class StartWindow : MonoBehaviour, IWindow
{
    public event Action Play;
    public event Action SelectDifficulty;

    [SerializeField] private GameObject _holder;
    [SerializeField] private Button _playButton;
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

    private void SubscribeToEvents()
    {
        _playButton.onClick.AddListener(OnPlayButtonPressed);
        _selectDifficultyButton.onClick.AddListener(OnSelectDifficultyButtonPressed);
    }

    private void OnSelectDifficultyButtonPressed()
    {
        SelectDifficulty?.Invoke();
    }

    private void OnPlayButtonPressed()
    {
        Play?.Invoke();
        SetOpen(false);
    } 

    public void SetOpen(bool open)
    {
        _holder.SetActive(open);
    }
}