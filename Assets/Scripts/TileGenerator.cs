using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileGenerator : MonoBehaviour, ITileGenerator
{
    [SerializeField] private Tile _tile;
    [SerializeField] private Tilemap _tileMap;
    
    public void Generate(Vector3Int position)
    {
        _tileMap.SetTile(position, _tile);
    }
}
