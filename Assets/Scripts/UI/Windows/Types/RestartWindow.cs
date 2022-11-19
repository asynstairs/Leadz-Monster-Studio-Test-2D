using LevelSignals;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class RestartWindow : MonoBehaviour, IWindow
{
    [SerializeField] private GameObject _holder;
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _selectDifficultyButton;
    [SerializeField] private bool _openOnStart;
    [Inject] private readonly SignalBus _signalBus;

    private void Start()
    {
        SetOpen(_openOnStart);
    }

    private void OnEnable()
    {
        SubscribeToButtonsEvents();
    }

    private void SubscribeToButtonsEvents()
    {
        _selectDifficultyButton.onClick.AddListener(OnSelectDifficultyButtonPressed);
        _restartButton.onClick.AddListener(OnRestartButtonPressed);
    }

    public void OnGameEnded(SignalGameEnded signalGameEnded)
    {
        SetOpen(true);
    }

    private void OnRestartButtonPressed()
    {
        _signalBus.Fire<SignalNeedRestart>();
        SetOpen(false);
    }

    private void OnSelectDifficultyButtonPressed()
    {
        _signalBus.Fire<SignalNeedDifficultySelect>();
    }
    
    public void SetOpen(bool open)
    {
        _holder.SetActive(open);
    }
}
