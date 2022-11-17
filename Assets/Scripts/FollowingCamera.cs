using UnityEngine;

[RequireComponent(typeof(Camera))]
public class FollowingCamera : MonoBehaviour
{
    [SerializeField] private GameObject _target;

    [SerializeField, Min(0f)] private float _velocity;

    private Transform _transform;

    private void Awake()
    {
        _transform = GetComponent<Transform>();
    }

    private void Update()
    {
        Vector3 position = new(_target.transform.position.x, _target.transform.position.y, _transform.position.z);
        transform.position = Vector3.Lerp(transform.position, position, Time.deltaTime * _velocity);
    }
}
