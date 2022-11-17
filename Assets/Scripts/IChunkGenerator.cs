using UnityEngine;

public interface IChunkGenerator
{
    public void Generate(Vector3Int start, Vector3Int size);
}