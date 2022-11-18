using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using Zenject;

public class ChunkPooler : MonoBehaviour
{
    [Inject] private IChunkGenerator _generator;

    public Vector3Int ChunkSize => _chunkSize;

    [SerializeField] private Vector3Int _chunkSize;
    
    public Vector3Int ChunksTiling => _chunkTiling;

    [SerializeField] private Vector3Int _chunkTiling;
    
    [SerializeField] private GameObject _player;

    [SerializeField, Min(0)] private int _deltaTilesPooling;

    private Vector3Int _bottomAnchor;
    private Vector3Int _topAnchor;
    
    private void Start()
    {
        _bottomAnchor = new(_chunkSize.x * _chunkTiling.x, 0);
        _topAnchor = new(_chunkSize.x * _chunkTiling.x, _chunkSize.y * _chunkTiling.y);

        InstantiateField();
        GenerateField(Vector3Int.zero, _chunkTiling);
    }

    private void InstantiateField()
    {
        var count = _chunkTiling.x * _chunkTiling.y;
        
        for (int x = 0; x < count; x++)
        {
            _generator.Instantiate(ChunkSize);
        }
    }

    private void GenerateField(Vector3Int start, Vector3Int tiling)
    {
        for (int x = 0; x < tiling.x; x++)
        {
            for (int y = 0; y < tiling.y; y++)
            {
                Vector3Int position = new(start.x + ChunkSize.x * x,  start.y + ChunkSize.y * y);
                _generator.GeneratePattern(position, ChunkSize);
            }
        }
    }

     private void Update()
     {
         if (NeedToGenerateBottom())
         {
             GenerateBottom();
         } 
         else if (NeedToGenerateTop())
         {
             GenerateTop();
         }
         
         if (NeedToGenerateRight())
         {
             GenerateRight();
         }
     }
    
     private bool NeedToGenerateBottom()
         => _player.transform.position.y - _bottomAnchor.y < _deltaTilesPooling;
    
     private bool NeedToGenerateTop()
         => _topAnchor.y - _player.transform.position.y < _deltaTilesPooling;
    
     private bool NeedToGenerateRight()
         => _topAnchor.x - _player.transform.position.x < _deltaTilesPooling;
    
     private void GenerateBottom()
     {
         _bottomAnchor -= new Vector3Int(0, _chunkSize.y);
         _topAnchor -= new Vector3Int(0, _chunkSize.y);
    
         Vector3Int tiling = new(_chunkTiling.x, 1);
    
         Vector3Int position = new((int)_player.transform.position.x, _bottomAnchor.y);
         GenerateField(position, tiling);
     }
    
     private void GenerateTop()
     {
         _topAnchor += new Vector3Int(0, _chunkSize.y);
         
         Vector3Int tiling = new(_chunkTiling.x, 1);
         
         Vector3Int position = new((int)_player.transform.position.x, _topAnchor.y);
         GenerateField(position, tiling);
         
         _bottomAnchor += new Vector3Int(0, _chunkSize.y);
     }
    
     private void GenerateRight()
     {
         GenerateField(_bottomAnchor, _chunkTiling);
         _topAnchor += new Vector3Int(_chunkSize.x * _chunkTiling.x, 0);
         _bottomAnchor += new Vector3Int(_chunkSize.x * _chunkTiling.x, 0);
    }
}