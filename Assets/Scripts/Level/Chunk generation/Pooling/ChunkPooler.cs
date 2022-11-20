using LevelSignals;
using UnityEngine;
using Zenject;

/// <summary>
/// Creates and removes chunks. A chunk is the set of tiles.
/// </summary>
public class ChunkPooler : MonoBehaviour
{
    [SerializeField] private int _countChunks;
    [SerializeField, Min(0)] private int _deltaTilesPooling;
    [SerializeField] private Vector3Int _startPosition;
    [Inject] private IChunkGenerator _generator;

    private int _lastTileSpawnedXCoord;
    private Transform _playerTransform;
    
    [Inject]
    private void GetPlayerTransform(TagPlayer player)
    {
        _playerTransform = player.GetComponent<Transform>();
    }
    
    private void Start()
    {
        InstantiateChunks();
        GenerateChunks(_countChunks);
    }

    public void OnGameRestarted(SignalGameRestarted signalGameRestarted)
    {
        _generator.Reset();
        _lastTileSpawnedXCoord = 0;
        GenerateChunks(_countChunks);
    }

    private void InstantiateChunks()
    {
        for (int _ = 0; _ < _countChunks; ++_)
        {
            _generator.InstantiateChunk();
        }
    }
    
    private void GenerateChunks(int countChunks)
    {
        for (int i = 0; i < countChunks; ++i)
        {
            GenerateNextChunk();
        }
    }

    private void GenerateNextChunk()
    {
        Vector3Int nextChunkStartPosition = new (_startPosition.x + _lastTileSpawnedXCoord, _startPosition.y);
        _generator.GeneratePatternInInstantiatedChunk(nextChunkStartPosition);
        _lastTileSpawnedXCoord += _generator.ChunkSize.x;
    }
    
     private void Update()
     {
         if (NeedToGenerateChunkToRight())
         {
             GenerateChunkToRight();
         }
     }
     
     private bool NeedToGenerateChunkToRight()
         => _lastTileSpawnedXCoord - _playerTransform.position.x < _deltaTilesPooling;

     private void GenerateChunkToRight()
     {
         GenerateNextChunk();
     }
}