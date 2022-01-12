using UnityEngine;

public class Coin : MonoBehaviour
{
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
            _objectPoolingManagerInstance.ReturnToPool("coin", gameObject);
        }

        transform.position += _gameControllerInstance.GetMoveLeftSpeed() * Time.deltaTime * Vector3.left;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _soundManagerInstance.Play(SoundManager.SoundTags.CoinPickup);
            ScoreManager.GetInstance().Coin_Collected();
            _objectPoolingManagerInstance.ReturnToPool("coin", gameObject);
        }
    }

}
