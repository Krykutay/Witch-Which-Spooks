using System;
using System.Collections;
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
        StartCoroutine(Lifetime((transform.position.x - _gameControllerInstance.objectsLeftSideDepsawnPointOnX) / _gameControllerInstance.GetMoveLeftSpeed()));
    }

    IEnumerator Lifetime(float duration)
    {
        yield return new WaitForSeconds(duration);
        _objectPoolingManagerInstance.ReturnToPool(_powerupPoolKey, this.gameObject);
    }

    void Update()
    {
        if (_gameControllerInstance.GetCurrentState() == State.Playing)
        {
            transform.position += Vector3.left * Time.deltaTime * _gameControllerInstance.GetMoveLeftSpeed();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _soundManagerInstance.Play(SoundManager.SoundTags.PotionPickup);
            Collected?.Invoke(this);

            _objectPoolingManagerInstance.ReturnToPool(_powerupPoolKey, this.gameObject);
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
