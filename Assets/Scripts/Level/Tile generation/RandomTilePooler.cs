using System.Collections.Generic;
using LevelSignals;
using UnityEngine;
using Zenject;

/// <summary>
/// Creates a single tile.
/// </summary>
public class RandomTilePooler : MonoBehaviour
{
    public readonly Queue<Transform> Tiles = new();

    [SerializeField] private GameObject _tilePrefab;
    [SerializeField] private GameObject _tilesFolder;
    [SerializeField] private int _tilesCount;
    [SerializeField] private int _spawnTileIntervalXCoord;
    [SerializeField] [Min(0)] private int _deltaPoolingTiles;
    [SerializeField] private int _minHeightSpawn;
    [SerializeField] private int _maxHeightSpawn;
    [Inject] private TagPlayer _player;

    private Transform _playerTransform;
    private int _lastTileSpawnedXCoord;

    public void OnGameRestarted(SignalGameRestarted signalGameRestarted)
    {
        var tiles = Tiles.ToArray();

        for (int i = 0; i < tiles.Length; i++)
        {
            tiles[i].gameObject.SetActive(false);
        }

        _lastTileSpawnedXCoord = 0;
        _lastTileSpawnedXCoord += _spawnTileIntervalXCoord;
    }

    private void Awake()
    {
        Init();
        InstantiateTile();
    }

    private void Init()
    {
        _playerTransform = _player.GetComponent<Transform>();
        _lastTileSpawnedXCoord += _spawnTileIntervalXCoord;
    }

    private void Update()
    {
        if (NeedToGenerateToRight())
        {
            GenerateNextTile();
        }
    }

    private void InstantiateTile()
    {
        for (int i = 0; i < _tilesCount; i++)
        {
            GameObject go = Instantiate(_tilePrefab, _tilesFolder.transform);
            Tiles.Enqueue(go.transform);
            go.SetActive(false);
        }   
    }

    private void GenerateNextTile()
    {
        var tile = Tiles.Dequeue();
        Tiles.Enqueue(tile);
        int x = _lastTileSpawnedXCoord;
        int y = UnityEngine.Random.Range(_minHeightSpawn, _maxHeightSpawn);
        tile.position = new Vector3Int(x, y);
        tile.gameObject.SetActive(true);
        _lastTileSpawnedXCoord += _spawnTileIntervalXCoord;
    }
    
    private bool NeedToGenerateToRight()
        => _lastTileSpawnedXCoord - _playerTransform.position.x < _deltaPoolingTiles;
}
