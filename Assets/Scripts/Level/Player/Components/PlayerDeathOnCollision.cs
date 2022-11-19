using UnityEngine;
using Zenject;

/// <summary>
/// This component of the player makes the game over
/// if the player collides a TagObstacle.
/// </summary>
[DisallowMultipleComponent]
public class PlayerDeathOnCollision : MonoBehaviour
{
    [Inject] private readonly SignalBus _signalBus;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<TagObstacle>())
        {
            _signalBus.AbstractFire<SignalGamemodeCollisionDeathTriggered>();
        }
    }
}
