using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileGenerator : MonoBehaviour, ITileGenerator
{
    [SerializeField] private GameObject _tile;
    [SerializeField] private GameObject _folder;

    public readonly Queue<Transform> Tiles = new();

    public void Instantiate(uint count)
    {
        for (var _ = 0; _ < count; _++)
        {
            GameObject go = Instantiate(_tile, Vector3.zero, quaternion.identity, _folder.transform);
            go.SetActive(false);
            Tiles.Enqueue(go.GetComponent<Transform>());
        }
    }

    public void Generate(Vector3Int position)
    {
        Transform transform = Tiles.Dequeue();
        transform.position = position;
        Tiles.Enqueue(transform);
    }
}
