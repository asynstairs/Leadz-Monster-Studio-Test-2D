using UnityEngine;
using Zenject;

[DisallowMultipleComponent]
[RequireComponent(typeof(Transform))]
public class RespawnOnGameEndedProvider : MonoBehaviour
{
    [SerializeField] private Transform _respawnTransform;

    [Inject] private LevelController _levelController;

    private Transform _transform;

    private void OnEnable()
    {
        SubscribeToEvents();
    }

    private void OnDisable()
    {
        UnsubscribeFromEvents();
    }

    private void Awake()
    {
        _transform = GetComponent<Transform>();
    }

    private void SubscribeToEvents()
    {
        _levelController.GameEnded += OnGameEnded;
    }
    
    private void UnsubscribeFromEvents()
    {
        _levelController.GameEnded -= OnGameEnded;
    }
    
    private void OnGameEnded()
    {
        _transform.position = _respawnTransform.position;
    }
}