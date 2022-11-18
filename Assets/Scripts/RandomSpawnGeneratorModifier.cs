using System.Collections;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;
public class RandomSpawnGeneratorModifier : TileGenerator
{
    [Inject] private ChunkPooler _chunkPooler;

    [SerializeField] private ITileGenerator _tileGenerator;

    [SerializeField] private uint _timeoutSeconds;

    [SerializeField] private Transform _player;

    private IEnumerator Spawn()
    {
        while (true)
        {
            int x = (int)_player.position.x + _chunkPooler.ChunkSize.x * Random.Range(1, 2);
            int y = (int)_player.position.y + _chunkPooler.ChunkSize.y * Random.Range(0, 1);
            
            _tileGenerator.Generate(new(x, y));
            
            yield return new WaitForSeconds(_timeoutSeconds);
        }
    }
}