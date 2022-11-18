using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
    public event Action GamePaused;
    public event Action GameResumed;
    public event Action GameRestarted;
    public event Action GameEnded;
    public event Action FeaturesConstructed;

    [Inject] private PauseWindow _pauseWindow;
    [Inject] private IGamemode _gamemode;

    private AttemptsFeatureOneshot _attemptsFeature;
    private LevelData _currentLevelData;

    private void Start()
    {
        OnPause();
        ContructGamemodeFeatures();
    }

    private void OnEnable()
    {
        SubscribeToEvents();
    }

    [Inject]
    private void SubscribeToEvents()
    {
        _pauseWindow.Play += OnPlay;
        _pauseWindow.Pause += OnPause;
        _pauseWindow.Restart += OnRestart;
        _gamemode.GameEnded += OnGamemodeGameEnded;
    }
    
    private void ContructGamemodeFeatures()
    {
        _currentLevelData =  BinarySerializer.Deserialize();
        _attemptsFeature = new();
        _attemptsFeature.Result.Value = _currentLevelData.Attempts;
        
        _gamemode.Features.Value = new List<IFeatureOneshot>()
        {
            _attemptsFeature
        };

        _gamemode.FeaturesOnUpdate.Value = new List<IFeatureOnUpdate>()
        {
            new TimeFeatureOnUpdate()
        };
        
        FeaturesConstructed?.Invoke();
    }

    private void Update()
    {
        foreach (var featureOnUpdate in _gamemode.FeaturesOnUpdate.Value)
        {
            featureOnUpdate.ExecuteOnUpdate(Time.deltaTime);
        }
    }

    private void OnRestart()
    {
        GameRestarted?.Invoke();
    }

    private void OnGamemodeGameEnded()
    {
        _attemptsFeature.Execute();
        _currentLevelData.Attempts = _attemptsFeature.Result.Value;

        foreach (var feature in _gamemode.FeaturesOnUpdate.Value)
        {
            feature.Reset();
        }

        GameEnded?.Invoke();
        
        BinarySerializer.SerializeAsync(_currentLevelData);
    }

    private void OnPause()
    {
        Time.timeScale = 0f;
        GamePaused?.Invoke();
    }

    private void OnPlay()
    {
        Time.timeScale = 1f;
        GameResumed?.Invoke();
    }
}
