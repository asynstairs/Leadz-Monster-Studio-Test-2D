using LevelSignals;
using UnityEngine;
using Zenject;

public class PlayerCollisionEventBroadcaster : MonoBehaviour
{
    [Inject] private readonly SignalBus _signalBus;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<TagObstacle>())
        {
            _signalBus.AbstractFire<SignalPlayerCollidedObstacle>(new (){Obstacle = collision.gameObject.GetComponent<TagObstacle>()});
        }
    }
}
