using UnityEngine;

public class CreateCoin : MonoBehaviour
{
    ObjectPoolingManager _objectPoolingManagerInstance;
    GameController _gameControllerInstance;

    float _coinSpawnTimer;

    void Awake()
    {
        _objectPoolingManagerInstance = ObjectPoolingManager.Instance;
        _gameControllerInstance = GameController.GetInstance();
    }

    void OnEnable()
    {
        Enemy.SpawnCoinOnKilled += Enemy_SpawnCoinOnKilled;
    }

    void OnDisable()
    {
        Enemy.SpawnCoinOnKilled -= Enemy_SpawnCoinOnKilled;
    }

    void Enemy_SpawnCoinOnKilled(Enemy enemy)
    {
        CoinSpawn(enemy.transform.position);
    }

    void Update()
    {
        HandleCoinSpawning();
    }

    void HandleCoinSpawning()
    {
        _coinSpawnTimer -= Time.deltaTime;
        if (_coinSpawnTimer < 0)
        {
            _coinSpawnTimer = _gameControllerInstance.coinSpawnTimerMax;

            float EnrichedSpawnPositionX = Random.Range(10f, 13f);
            Vector2 spawnPoint = new Vector2(EnrichedSpawnPositionX, Random.Range(-4.2f, 5f));
            CoinSpawn(spawnPoint);
        }
    }

    void CoinSpawn(Vector2 spawnPoint)
    {
        GameObject coin = _objectPoolingManagerInstance.Get("coin");
        coin.transform.rotation = Quaternion.Euler(0, 0, 0);
        coin.transform.position = spawnPoint;
        coin.gameObject.SetActive(true);
    }

}
