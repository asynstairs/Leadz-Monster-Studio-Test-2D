using UnityEngine;

namespace LevelSignals
{
    public struct SignalPlayerCollidedObstacle : ISignalPlayerCollided
    {
        public TagObstacle Obstacle { get; set;  }
    }
}