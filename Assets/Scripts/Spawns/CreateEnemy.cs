using UnityEngine;

public class CreateEnemy : MonoBehaviour
{
    ObjectPoolingManager _objectPoolingManagerInstance;
    GameController _gameControllerInstance;

    float _enemySpawnTimer;
    
    void Awake()
    {
        _objectPoolingManagerInstance = ObjectPoolingManager.Instance;
        _gameControllerInstance = GameController.GetInstance();
    }

    void Update()
    {
        HandleEnemySpawning();
    }

    void HandleEnemySpawning()
    {
        _enemySpawnTimer -= Time.deltaTime;
        if (_enemySpawnTimer < 0)
        {
            _enemySpawnTimer = _gameControllerInstance.enemySpawnTimerMax;
            EnemySpawn();
        }
    }

    void EnemySpawn()
    {
        float EnrichedSpawnPositionX = Random.Range(10f, 13f);
        Vector2 spawnPoint = new Vector2(EnrichedSpawnPositionX, Random.Range(-0.5f, 5f));

        GameObject enemy;

        enemy = _objectPoolingManagerInstance.Get("enemy");
        enemy.transform.rotation = Quaternion.Euler(0, 180, 0);
        enemy.transform.position = spawnPoint;
        enemy.gameObject.SetActive(true);
    }
}
