using System;
using System.Collections.Generic;
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
        _gamemode.Features.Value = new List<IFeatureOneshot>()
        {
            new AttemptsFeatureOneshot()
        };
        
        _gamemode.FeaturesOnUpdate.Value = new List<IFeatureOnUpdate>()
        {
            new TimeFeature()
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
        foreach (var feature in _gamemode.Features.Value)
        {
            feature.Execute();
        }

        GameEnded?.Invoke();
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
