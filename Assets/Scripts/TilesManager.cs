using System.Collections;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;
public class TilesManager : MonoBehaviour
{
    [Inject] private ITileGenerator _tileGenerator;
    [Inject] private ChunkPooler _chunkPooler;
    
    [SerializeField] private uint _tilesIntevalRespawnObstacle;

    // private IEnumerator SpawnObstacle()
    // {
    //     int x = Random.Range(0, _chunkPooller.ChunkSize.x * _chunkPooller.ChunksTiling.x);
    //     int y = Random.Range(0, _chunkPooller.ChunkSize.y * _chunkPooller.ChunksTiling.y);
    //     
    //     _tileGenerator.Generate(new(x, y));
    // }
    
    
}