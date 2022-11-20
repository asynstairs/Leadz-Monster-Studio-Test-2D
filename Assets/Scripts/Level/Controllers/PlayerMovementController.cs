using System.Collections;
using LevelSignals;
using UnityEngine;
using Zenject;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] private float _horizontalSpeed;
    [SerializeField] private float _verticalSpeed;
    [SerializeField] private float _verticalSpeedIncreaseTimeoutSeconds;
    [SerializeField] private float _verticalVelocityIncrease;

    private PlayerInputSystem _playerInputSystem;
    private Rigidbody2D _rigidbody;
    private bool _canMove;
    private float _startVerticalSpeed;

    public void OnDifficultySelectionChanged(SignalDifficultyChanged signalDifficultyChanged)
    {
        _horizontalSpeed = signalDifficultyChanged.Difficulty.HorizontalSpeed;
        _startVerticalSpeed = signalDifficultyChanged.Difficulty.HorizontalSpeed;
    }

    public void OnGameRestarted(SignalGameRestarted signalGameRestarted)
    {
        _verticalSpeed = _startVerticalSpeed;
    }
    
    private void Start()
    {
        Init();
        StartCoroutine(IncreaseVerticalVelocity());
    }

    private void Init()
    {
        _startVerticalSpeed = _verticalSpeed;
        _canMove = true;
        _playerInputSystem = new();
        _playerInputSystem.Enable();
    }

    [Inject]
    private void GetPlayerRigidbody(TagPlayer player)
    {
        _rigidbody = player.GetComponent<Rigidbody2D>();
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
