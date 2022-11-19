using LevelSignals;
using UnityEngine;
using Zenject;

/// <summary>
/// The main enter point of a level. Decides whether the game restarts, pauses, ends and so on.
/// To check a level passing ending the LevelController uses Gamemodes and their features.
/// Only the LevelController mutates the Time class properties such as .timeScale.
/// </summary>
[DisallowMultipleComponent]
public class LevelController : MonoBehaviour
{
    [Inject] private IGamemode _gamemode;
    [Inject] private readonly SignalBus _signalBus;
    [Inject] private AttemptsFeatureOneshot _attemptsFeature;
    
    private LevelData _currentLevelData;
    private bool _paused;
    
    public void OnNeedRestart(SignalNeedRestart signalNeedRestart)
    {
        OnPlayerRestart();
    }

    public void OnGameEnded(SignalGameEnded signalGameEnded)
    {
        _attemptsFeature.Execute();
        _currentLevelData.Attempts = _attemptsFeature.Result.Value;

#pragma warning disable CS4014 
        BinarySerializer.Serialize(_currentLevelData); // we just asyncly send info, we never want to await
#pragma warning restore CS4014

        TogglePaused();
    }

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        Time.timeScale = 0f;
        _currentLevelData = BinarySerializer.Deserialize();
    }

    private void Update()
    {
        foreach (var featureOnUpdate in _gamemode.FeaturesOnUpdate.Value)
        {
            featureOnUpdate.ExecuteOnUpdate(Time.deltaTime);
        }
    }

    private void OnPlayerRestart()
    {
        TogglePaused();
        _signalBus.Fire<SignalGameRestarted>();
        
        foreach (var feature in _gamemode.FeaturesOnUpdate.Value)
        {
            feature.Reset();
        }
    }

    private void TogglePaused()
    {
        Time.timeScale = Time.timeScale == 0f ? 1f : 0f;
    }
}
