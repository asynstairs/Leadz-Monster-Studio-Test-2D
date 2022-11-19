using UnityEngine;

public interface IChunkGenerator
{
    public Vector3Int ChunkSize { get; }
    public void Instantiate();
    public void GeneratePattern(Vector3Int start);
    public void Reset();
}