using System;
using System.Collections;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovementProvider : MonoBehaviour
{
    [Inject] private DifficultiesSelectionWindow _difficultiesSelectionWindow;
    
    [SerializeField] private float _horizontalSpeed;
    [SerializeField] private float _verticalSpeed;

    [SerializeField] private float _verticalSpeedIncreaseTimeoutSeconds;
    [SerializeField] private float _verticalVelocityIncrease;

    private PlayerInputSystem _playerInputSystem;
    private Rigidbody2D _rigidbody;
    private bool _canMove;

    private void OnEnable()
    {
        _playerInputSystem.Enable();
        SubscribeToEvents();
    }
    
    private void OnDisable()
    {
        _playerInputSystem.Disable();
        UnsubscribeFromEvents();
    }

    private void SubscribeToEvents()
    {
        _difficultiesSelectionWindow.DifficultyChanged += OnDifficultySelectionChanged;
    }

    private void UnsubscribeFromEvents()
    {
        _difficultiesSelectionWindow.DifficultyChanged -= OnDifficultySelectionChanged;
    }

    private void OnDifficultySelectionChanged(DifficultySciptableObject description)
    {
        Debug.Log($"pl: {description.HorizontalSpeed}");
        _horizontalSpeed = description.HorizontalSpeed;
    }

    private void Awake()
    {
        Initialize();
        StartCoroutine(IncreaseVerticalVelocity());
    }

    private void Initialize()
    {
        _canMove = true;
        _playerInputSystem = new();
        _rigidbody = GetComponent<Rigidbody2D>();
    }
    
    private IEnumerator IncreaseVerticalVelocity()
    {
        while (true)
        {
            _verticalSpeed += _verticalVelocityIncrease;
            yield return new WaitForSeconds(_verticalSpeedIncreaseTimeoutSeconds);
        }
    }
    
    private void FixedUpdate()
    {
        if (_canMove == false)
        {
            return;
        }
        
        MoveVertically();
        MoveHorizontally();
    }

    private void MoveVertically()
    {
        if (_playerInputSystem.Air.Up.IsPressed())
        {
            _rigidbody.AddForce(_verticalSpeed * Vector2.up);
        }
        else
        {
            _rigidbody.AddForce(_verticalSpeed * Vector2.down);
        }
    }

    private void MoveHorizontally()
    {
        _rigidbody.velocity = _horizontalSpeed * Time.fixedDeltaTime * Vector2.right;
    }
}
