using UnityEngine;

public interface IChunkGenerator
{
    public void Instantiate(Vector3Int size);
    public void GeneratePattern(Vector3Int start, Vector3Int size);
}