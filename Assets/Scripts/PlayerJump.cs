using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJump : MonoBehaviour
{
    [SerializeField] float _jumpAmount = 6f;

    Rigidbody2D _witchRigidBody;
    PlayerInput _playerInput;
    InputAction _jumpAction;

    GameController _gameControllerInstance;
    SoundManager _soundManagerInstance;

    Vector2 _lookDir;
    float _angle;
    Quaternion _rotation;

    void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        _jumpAction = _playerInput.actions["Jump"];

        _witchRigidBody = GetComponent<Rigidbody2D>();

        _soundManagerInstance = SoundManager.GetInstance();
        _gameControllerInstance = GameController.GetInstance();
    }

    void OnEnable()
    {
        _jumpAction.started += Jump;
    }

    void OnDisable()
    {
        _jumpAction.started -= Jump;
    }


    void Update()
    {
        if (_gameControllerInstance.currentState != State.Playing)
            return;

        _lookDir.Set(16, Mathf.Clamp(_witchRigidBody.velocity.y, -6f, 6f));
        _lookDir.Normalize();

        _angle = Mathf.Atan2(_lookDir.y, _lookDir.x) * Mathf.Rad2Deg;
        _rotation = Quaternion.AngleAxis(_angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, _rotation, Time.deltaTime * 5f);
    }

    void Jump(InputAction.CallbackContext context)
    {
        if (_gameControllerInstance.currentState == State.Playing)
        {
            _soundManagerInstance.Play(SoundManager.SoundTags.PlayerJump);
            _witchRigidBody.velocity = Vector2.up * _jumpAmount;
        }
    }
}
