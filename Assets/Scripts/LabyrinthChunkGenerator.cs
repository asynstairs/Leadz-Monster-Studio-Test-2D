using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class LabyrinthChunkGenerator : MonoBehaviour, IChunkGenerator
{
    [SerializeField] private GameObject _tile;
    [SerializeField] private GameObject _tilesFolder;

    [SerializeField, Range(0f, 1f)] private float _holesProbability;
    [SerializeField, Min(0)] private int _holeWidth;

    public readonly Queue<Transform> Tiles = new();

    public void GeneratePattern(Vector3Int start, Vector3Int size)
    {
        Dictionary<string, Vector3Int> wallsJointPoints = new()
        {
            { "LeftEdgeBottom", start },
            { "LeftEdgeMiddle", new Vector3Int(start.x, start.y + size.y / 2) },
            { "LeftEdgeTop", new Vector3Int(start.x, start.y + size.y) },
            { "MiddleUpperLineBottom", new Vector3Int(start.x + size.x / 3, start.y + size.y / 2) },
            { "MiddleUpperLineTop", new Vector3Int(start.x + size.x / 3, start.y + size.y) },
            { "MiddleLowerLineBottom", new Vector3Int(start.x + size.x / 3 * 2, start.y) },
            { "MiddleLowerLineTop", new Vector3Int(start.x + size.x / 3 * 2, start.y + size.y / 2) },
            { "RightEdgeBottom", new Vector3Int(start.x + size.x, start.y) },
            { "RightEdgeMiddle", new Vector3Int(start.x + size.x, start.y + size.y / 2) },
            { "RightEdgeTop", new Vector3Int(start.x + size.x, start.y + size.y) },
        };

        Vector3Int offset = new(0, 1);

        GenerateRow(wallsJointPoints["LeftEdgeBottom"], size.x);
        GenerateColumn(wallsJointPoints["LeftEdgeBottom"] + offset, size.y - 1);
        GenerateRow(wallsJointPoints["LeftEdgeMiddle"], size.x);
        GenerateColumn(wallsJointPoints["MiddleLowerLineBottom"] + offset, size.y / 2);
        GenerateColumn(wallsJointPoints["MiddleUpperLineBottom"] + offset, size.y / 2 - 1);
        GenerateColumn(wallsJointPoints["RightEdgeBottom"], size.x);
    }

    public void Instantiate(Vector3Int size)
    {
        var count = size.x * size.y;
        
        for (int i = 0; i < count; i++)
        {
            GameObject go = Instantiate(_tile, Vector3.zero, quaternion.identity, _tilesFolder.transform);
            go.SetActive(false);
            Tiles.Enqueue(go.transform);
        }
    }

    private void GenerateColumn(Vector3Int start, int height)
    {
        for (int i = 0; i < height; i++)
        {
            if (Random.Range(0f, 1f) > _holesProbability)
            {
                Transform transform = Tiles.Dequeue();
                transform.position = new(start.x, start.y + i);
                Tiles.Enqueue(transform);
                transform.gameObject.SetActive(true);
            }
            else
            {
                i += _holeWidth;
            }
        }
    }

    private void GenerateRow(Vector3Int start, int width)
    {
        for (int i = 0; i < width; i++)
        {
            if (Random.Range(0f, 1f) > _holesProbability)
            {
                Transform transform = Tiles.Dequeue();
                transform.position = new(start.x + i, start.y);
                Tiles.Enqueue(transform);
                transform.gameObject.SetActive(true);
            }
            else
            {
                i += _holeWidth;
            }
        }
    }
}
