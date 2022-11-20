using LevelSignals;
using UnityEngine;
using Zenject;

/// <summary>
/// Respawns the player if fired the game restart signal.
/// </summary>
public class RespawnPlayerController
{
    private readonly Transform _playerTransform;
    private readonly Transform _respawnPointTransform;

    [Inject]
    public RespawnPlayerController(TagPlayer player, TagPlayerRespawnPoint playerRespawnPoint)
    {
        _playerTransform = player.gameObject.GetComponent<Transform>();
        _respawnPointTransform = playerRespawnPoint.gameObject.GetComponent<Transform>();
    }

    public void OnGameRestart(SignalGameRestarted signalGameReload)
    {
        _playerTransform.position = _respawnPointTransform.position;
    }
}