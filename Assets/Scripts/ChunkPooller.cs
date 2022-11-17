using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using Zenject;

public class ChunkPooller : MonoBehaviour
{
    [Inject] private IChunkGenerator _generator;

    public Vector3Int ChunkSize => _chunkSize;

    [SerializeField] private Vector3Int _chunkSize;
    
    public Vector3Int ChunksTiling => _chunkTiling;

    [SerializeField] private Vector3Int _chunkTiling;
    
    [SerializeField] private GameObject _player;


    private Vector3Int _minRightBottomTilePosition;
    private Vector3Int _maxRightBottomTilePosition;

    [SerializeField] private int _deltaDistance;

    private void Start()
    {
        _minRightBottomTilePosition = new(_chunkSize.x * _chunkTiling.x, 0);
        _maxRightBottomTilePosition = new(_chunkSize.x * _chunkTiling.x, _chunkSize.y * _chunkTiling.y);
        GenerateField(Vector3Int.zero, _chunkTiling);
    }

    private void GenerateField(Vector3Int start, Vector3Int tiling)
    {
        for (int x = 0; x < tiling.x; x++)
        {
            for (int y = 0; y < tiling.y; y++)
            {
                Vector3Int position = new(start.x + ChunkSize.x * x,  start.y + ChunkSize.y * y);
                _generator.Generate(position, ChunkSize);
            }
        }
    }

    private void Update()
    {
        if (NeedToGenerateBottom())
        {
            GenerateBottom();
        } 
        // else if (NeedToGenerateTop())
        // {
        //     _maxRightBottomTilePosition += new Vector3Int(0, _chunkSize.y);
        // }
        //
        // if (NeedToGenerateRight())
        // {
        //     
        // }
    }

    private bool NeedToGenerateBottom()
        => _player.transform.position.y - _minRightBottomTilePosition.y < _deltaDistance;
    
    private bool NeedToGenerateTop()
        => _maxRightBottomTilePosition.y - _player.transform.position.y < _deltaDistance;
    
    private bool NeedToGenerateRight()
        => _maxRightBottomTilePosition.x - _player.transform.position.x < _deltaDistance;

    private void GenerateBottom()
    {
        _minRightBottomTilePosition -= new Vector3Int(0, _chunkSize.y);
        _maxRightBottomTilePosition -= new Vector3Int(0, _chunkSize.y);

        Vector3Int tiling = new(_chunkTiling.x, 1);
        GenerateField(_minRightBottomTilePosition, tiling);
    }
    
}