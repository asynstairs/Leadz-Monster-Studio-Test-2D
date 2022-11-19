using UnityEngine;
using Zenject;

[RequireComponent(typeof(Camera))]
public class FollowingPlayerCamera : MonoBehaviour
{
    [SerializeField, Min(0f)] private float _velocity;
    [Inject] private TagPlayer _player;
    
    private Transform _cameraTransform;
    private Transform _playerTransform;

    [Inject] private readonly SignalBus _signalBus;

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        _cameraTransform = GetComponent<Transform>();
        _playerTransform = _player.GetComponent<Transform>();
    }

    private void Update()
    {
        Vector3 position = new(_playerTransform.position.x, _playerTransform.position.y, _cameraTransform.position.z);
        transform.position = Vector3.Lerp(transform.position, position, Time.deltaTime * _velocity);
    }
}
