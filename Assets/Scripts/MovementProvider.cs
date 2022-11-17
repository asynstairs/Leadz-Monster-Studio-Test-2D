using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MovementProvider : MonoBehaviour
{
    [SerializeField] private float _horizontalSpeed;
    [SerializeField] private float _verticalSpeed;

    private PlayerInputSystem _playerInputSystem;
    private Rigidbody2D _rigidbody;
    private bool _canMove;

    private void Awake()
    {
        _canMove = true;
        _playerInputSystem = new();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        _playerInputSystem.Enable();
    }

    private void OnDisable()
    {
        _playerInputSystem.Disable();
    }

    private void FixedUpdate()
    {
        if (_canMove == false)
        {
            return;
        }
        
        MoveUp();
        MoveRight();
    }

    private void MoveUp()
    {
        if (_playerInputSystem.Air.Up.IsPressed())
        {
            _rigidbody.AddForce(_verticalSpeed * Vector2.up);
        };
    }

    private void MoveRight()
    {
        _rigidbody.AddForce(_horizontalSpeed * Vector2.right);
    }
}
