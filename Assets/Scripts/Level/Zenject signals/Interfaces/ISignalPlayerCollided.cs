using UnityEngine;

namespace LevelSignals
{
    public interface ISignalPlayerCollided
    {
        public TagObstacle Obstacle { get; }
    }
}