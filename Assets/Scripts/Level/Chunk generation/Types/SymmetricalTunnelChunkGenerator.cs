using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// Creates the chunk.
/// </summary>
public class SymmetricalTunnelChunkGenerator : MonoBehaviour, IChunkGenerator
{
    public Vector3Int ChunkSize => _chunkSize;
    public readonly Queue<Transform> Tiles = new();

    [SerializeField] private GameObject _tile;
    [SerializeField] private GameObject _tilesHolder;
    [SerializeField] private int _tunnelColumnsMaxHeight;
    [SerializeField] private Vector3Int _chunkSize;

    private Vector3Int _lastUpperPoint;
    private Vector3Int _lastLowerPoint;
    
    public void Reset()
    {
        var tiles = Tiles.ToArray();
        
        for (int i = 0; i < tiles.Length; i++)
        {
            tiles[i].gameObject.SetActive(false);
        }
    }
    
    public void GeneratePatternInInstantiatedChunk(Vector3Int bottomLeftStartPoint)
    {
        for (int x = 0; x < ChunkSize.x; ++x)
        {
            int randomLineHeight = Random.Range(1, _tunnelColumnsMaxHeight);

            for (int y = 0; y < randomLineHeight; ++y)
            {
                _lastLowerPoint = new(bottomLeftStartPoint.x + x, bottomLeftStartPoint.y + y);
                _lastUpperPoint = new(_lastLowerPoint.x,   bottomLeftStartPoint.y + ChunkSize.y - _lastLowerPoint.y);
                SpawnTile(_lastLowerPoint);
                SpawnTile(_lastUpperPoint);
            }
        }
    }
    
    public void InstantiateChunk()
    {
        var count = ChunkSize.x * ChunkSize.y;
        
        for (int i = 0; i < count; ++i)
        {
            GameObject go = Instantiate(_tile, Vector3.zero, quaternion.identity, _tilesHolder.transform);
            go.SetActive(false);
            Tiles.Enqueue(go.transform);
        }
    }
    
    private void SpawnTile(Vector3Int position)
    {
        var tile = Tiles.Dequeue();
        Tiles.Enqueue(tile);
        tile.gameObject.SetActive(true);
        tile.transform.position = position;
    }
}

