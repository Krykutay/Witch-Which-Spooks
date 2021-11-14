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


    void FixedUpdate()
    {
        if (_gameControllerInstance.GetCurrentState() == State.Playing)
        {
            Vector3 lookDir = new Vector3(16, Mathf.Clamp(_witchRigidBody.velocity.y, -6f, 6f), 0);
            lookDir.Normalize();

            var angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
            var rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 5f);
        }
    }

    void Jump(InputAction.CallbackContext context)
    {
        if (_gameControllerInstance.GetCurrentState() == State.Playing)
        {
            _soundManagerInstance.Play(SoundManager.SoundTags.PlayerJump);
            _witchRigidBody.velocity = Vector2.up * _jumpAmount;
        }
    }
}
