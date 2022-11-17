using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ChunkGenerator : MonoBehaviour
{
    [SerializeField] private Vector2Int _chunkSize;

    [SerializeField] private Tilemap _tilemap;

    [SerializeField] private Tile _tile;

    [SerializeField, Min(1)] private uint _countHolesInChunk;
    
    public void GenerateChunk(Vector2Int start)
    {
        Dictionary<string, Vector2Int> _wallsJointPoints = new()
        {
            { "LeftEdgeBottom", start },
            { "LeftEdgeMiddle", new Vector2Int(start.x,  start.y + _chunkSize.y / 2) },
            { "LeftEdgeTop", new Vector2Int(start.x, start.y + _chunkSize.y) },
            { "MiddleUpperLineBottom", new Vector2Int(start.x + _chunkSize.x / 3, start.y + _chunkSize.y / 2) },
            { "MiddleUpperLineTop", new Vector2Int(start.x + _chunkSize.x / 3, start.y + _chunkSize.y) },
            { "MiddleLowerLineBottom", new Vector2Int(start.x + _chunkSize.x / 3 * 2, start.y) },
            { "MiddleLowerLineTop", new Vector2Int(start.x + _chunkSize.x / 3 * 2, start.y + _chunkSize.y / 2) },
            { "RightEdgeBottom", new Vector2Int(start.x + _chunkSize.x, start.y) },
            { "RightEdgeMiddle", new Vector2Int(start.x + _chunkSize.x, start.y + _chunkSize.y / 2) },
            { "RightEdgeTop", new Vector2Int(start.x + _chunkSize.x, start.y + _chunkSize.y) },
        };
        
        Vector2Int offset = new (0, 1);
        
        GenerateRow(_wallsJointPoints["LeftEdgeBottom"], _chunkSize.x);
        GenerateColumn(_wallsJointPoints["LeftEdgeBottom"] + offset,_chunkSize.y - 1);
        GenerateRow(_wallsJointPoints["LeftEdgeMiddle"], _chunkSize.x);
        GenerateColumn(_wallsJointPoints["MiddleLowerLineBottom"] + offset, _chunkSize.y / 2);
        GenerateColumn(_wallsJointPoints["MiddleUpperLineBottom"] + offset, _chunkSize.y / 2 - 1);
        GenerateColumn(_wallsJointPoints["RightEdgeBottom"], _chunkSize.x);
    }

    private void GenerateHoles()
    {
        
    }

    private void IsWallsJoint(Vector2Int start, Vector2Int position)
    {

    }
    
    private void GenerateColumn(Vector2Int start, int height)
    {
        for (int i = start.y; i < start.y + height; i++)
        {
            Vector3Int position = new(start.x, i);
            _tilemap.SetTile(position, _tile);
        }
    }

    private void GenerateRow(Vector2Int start, int width)
    {
        for (int i = start.x; i < start.x + width; i++)
        {
            Vector3Int position = new(i, start.y);
            _tilemap.SetTile(position, _tile);
        }
    }

    private void Start()
    {
        GenerateChunk(new(0,0));
    }
}
