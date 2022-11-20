using UnityEngine;

public interface IChunkGenerator
{
    public Vector3Int ChunkSize { get; }
    public void InstantiateChunk();
    public void GeneratePatternInInstantiatedChunk(Vector3Int bottomLeftStartPoint);
    public void Reset();
}