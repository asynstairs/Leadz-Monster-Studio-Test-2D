using System;
using UnityEngine;

public class CollisionEventBroadcaster : MonoBehaviour
{
    public event Action<Collision2D> CollisionEnter;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        CollisionEnter?.Invoke(collision);
    }
}
