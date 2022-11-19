using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// Creates the chunk.
/// </summary>
public class SymmethricalTunnelChunkGenerator : MonoBehaviour, IChunkGenerator
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

    public void GeneratePattern(Vector3Int start)
    {
        for (int x = 0; x < ChunkSize.x; x++)
        {
            int randomLineHeight = Random.Range(1, _tunnelColumnsMaxHeight);

            for (int y = 0; y < randomLineHeight; y++)
            {
                _lastLowerPoint = new(start.x + x, start.y + y);
                _lastUpperPoint = new(_lastLowerPoint.x,   start.y + ChunkSize.y - _lastLowerPoint.y);
                SpawnTile(_lastLowerPoint);
                SpawnTile(_lastUpperPoint);
            }
        }
    }
    

    private void SpawnTile(Vector3Int position)
    {
        var tile = Tiles.Dequeue();
        Tiles.Enqueue(tile);
        tile.gameObject.SetActive(true);
        tile.transform.position = position;
    }

    public void Instantiate()
    {
        var count = ChunkSize.x * ChunkSize.y;
        
        for (int i = 0; i < count; ++i)
        {
            GameObject go = Instantiate(_tile, Vector3.zero, quaternion.identity, _tilesHolder.transform);
            go.SetActive(false);
            Tiles.Enqueue(go.transform);
        }
    }
}

