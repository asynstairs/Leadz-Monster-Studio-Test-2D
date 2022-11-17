using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class LabyrinthChunkGenerator : MonoBehaviour, IChunkGenerator
{
    [SerializeField] private Tilemap _tilemap;

    [SerializeField] private Tile _tile;

    [SerializeField, Min(1)] private uint _countHoleRows;
    [SerializeField, Min(1)] private uint _holeRowWidth;

    public void Generate(Vector3Int start, Vector3Int size)
    {
        Dictionary<string, Vector3Int> wallsJointPoints = new()
        {
            { "LeftEdgeBottom", start },
            { "LeftEdgeMiddle", new Vector3Int(start.x,  start.y + size.y / 2) },
            { "LeftEdgeTop", new Vector3Int(start.x, start.y + size.y) },
            { "MiddleUpperLineBottom", new Vector3Int(start.x + size.x / 3, start.y + size.y / 2) },
            { "MiddleUpperLineTop", new Vector3Int(start.x + size.x / 3, start.y + size.y) },
            { "MiddleLowerLineBottom", new Vector3Int(start.x + size.x / 3 * 2, start.y) },
            { "MiddleLowerLineTop", new Vector3Int(start.x + size.x / 3 * 2, start.y + size.y / 2) },
            { "RightEdgeBottom", new Vector3Int(start.x + size.x, start.y) },
            { "RightEdgeMiddle", new Vector3Int(start.x + size.x, start.y + size.y / 2) },
            { "RightEdgeTop", new Vector3Int(start.x + size.x, start.y + size.y) },
        };
        
        Vector3Int offset = new (0, 1);
        
        GenerateRow(wallsJointPoints["LeftEdgeBottom"], size.x);
        GenerateColumn(wallsJointPoints["LeftEdgeBottom"] + offset,size.y - 1);
        GenerateRow(wallsJointPoints["LeftEdgeMiddle"], size.x);
        GenerateColumn(wallsJointPoints["MiddleLowerLineBottom"] + offset, size.y / 2);
        GenerateColumn(wallsJointPoints["MiddleUpperLineBottom"] + offset, size.y / 2 - 1);
        GenerateColumn(wallsJointPoints["RightEdgeBottom"], size.x);

        for (int i = 0; i < _countHoleRows; i++)
        {
            Vector3Int randomOffset = new Vector3Int(0, Random.Range(0, size.y));
            
            for (int j = 0; j < _holeRowWidth; j++)
            {
                Vector3Int widthOffset = randomOffset + new Vector3Int(0, j, 0);
                RemoveRow(start + widthOffset, size.x + 1);
            }
        }
    }

    private void RemoveRow(Vector3Int start, int width)
    {
        for (int i = 0; i < width; i++)
        {
            Vector3Int offset = new (i, 0, 0);
            _tilemap.SetTile(start + offset, null);
        }
    }

    private void GenerateColumn(Vector3Int start, int height)
    {
        for (int i = 0; i < height; i++)
        {
            Vector3Int position = new(start.x, start.y + i);
            _tilemap.SetTile(position, _tile);
        }
    }

    private void GenerateRow(Vector3Int start, int width)
    {
        for (int i = 0; i < width; i++)
        {
            Vector3Int position = new(start.x + i, start.y);
            _tilemap.SetTile(position, _tile);
        }
    }
}
