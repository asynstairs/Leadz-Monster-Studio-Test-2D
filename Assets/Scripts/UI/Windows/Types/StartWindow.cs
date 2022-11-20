using LevelSignals;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class StartWindow : MonoBehaviour, IWindow
{
    [SerializeField] private GameObject _holder;
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _selectDifficultyButton;
    [SerializeField] private bool _openOnStart;
    [Inject] private readonly SignalBus _signalBus;

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
        _signalBus.Fire<SignalNeedDifficultySelect>();
    }

    private void OnPlayButtonPressed()
    {
        _signalBus.Fire<SignalNeedRestart>();
        SetOpen(false);
    } 

    public void SetOpen(bool open)
    {
        _holder.SetActive(open);
    }
}