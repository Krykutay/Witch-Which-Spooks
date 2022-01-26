using System;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    public static event Action<Powerup> Collected;

    [SerializeField] bool _isBlue;
    [SerializeField] bool _isGreen;
    [SerializeField] bool _isRed;

    [SerializeField] float _durationBlue = 6f;
    [SerializeField] float _durationGreen = 8f;
    [SerializeField] float _durationRed = 1.3f;

    [SerializeField] string _powerupPoolKey;

    float _lifeTime;
    float _spawnTime;

    ObjectPoolingManager _objectPoolingManagerInstance;
    GameController _gameControllerInstance;
    SoundManager _soundManagerInstance;

    void Awake()
    {
        _objectPoolingManagerInstance = ObjectPoolingManager.Instance;
        _soundManagerInstance = SoundManager.GetInstance();
        _gameControllerInstance = GameController.GetInstance();
    }

    void OnEnable()
    {
        _spawnTime = Time.time;
        _lifeTime = (transform.position.x - _gameControllerInstance.objectsLeftSideDepsawnPointOnX) / _gameControllerInstance.GetMoveLeftSpeed();
    }

    void Update()
    {
        if (Time.time > _spawnTime + _lifeTime)
        {
            _objectPoolingManagerInstance.ReturnToPool(_powerupPoolKey, gameObject);
        }

        transform.position += _gameControllerInstance.GetMoveLeftSpeed() * Time.deltaTime * Vector3.left;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _soundManagerInstance.Play(SoundManager.SoundTags.PotionPickup);
            Collected?.Invoke(this);

            _objectPoolingManagerInstance.ReturnToPool(_powerupPoolKey, gameObject);
        }
    }

    public bool GetIsBlue() => _isBlue;

    public bool GetIsGreen() => _isGreen;

    public bool GetIsRed() => _isRed;

    public float GetDuration()
    {
        if (_isBlue)
            return _durationBlue;
        else if (_isGreen)
            return _durationGreen;
        else
            return _durationRed;
    }
}
