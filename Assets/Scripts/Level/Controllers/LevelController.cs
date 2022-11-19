using System.Collections.Generic;
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
    [Inject] private RestartWindow _restartWindow;
    [Inject] private IGamemode _gamemode;
    [Inject] private readonly SignalBus _signalBus;
    
    private AttemptsFeatureOneshot _attemptsFeature;
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

        BinarySerializer.SerializeAsync(_currentLevelData);

        TogglePaused();
    }

    private void Awake()
    {
        Time.timeScale = 0f;
    }

    private void Start()
    {
        _currentLevelData =  BinarySerializer.Deserialize();
        _attemptsFeature = new();
        _attemptsFeature.Result.Value = _currentLevelData.Attempts;
    }
    
    [Inject]
    private void ContructGamemodeFeatures()
    {
        _gamemode.Features.Value = new List<IFeatureOneshot>()
        {
            _attemptsFeature
        };

        _gamemode.FeaturesOnUpdate.Value = new List<IFeatureOnUpdate>()
        {
            new TimeFeatureOnUpdate()
        };
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
