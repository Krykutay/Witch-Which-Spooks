using UnityEngine;

public class CreatePowerup : MonoBehaviour
{
    ObjectPoolingManager _objectPoolingManagerInstance;
    GameController _gameControllerInstance;

    float _powerupSpawnTimer;

    const int _POWERUP_TYPES_COUNT = 3;

    void Awake()
    {
        _objectPoolingManagerInstance = ObjectPoolingManager.Instance;
        _gameControllerInstance = GameController.GetInstance();
    }

    void OnEnable()
    {
        Enemy.SpawnPowerupOnKilled += Enemy_SpawnPowerupOnKilled;
    }

    void OnDisable()
    {
        Enemy.SpawnPowerupOnKilled -= Enemy_SpawnPowerupOnKilled;
    }

    void Enemy_SpawnPowerupOnKilled(Enemy enemy)
    {
        PowerupSpawn(enemy.transform.position);
    }

    void Update()
    {
        HandlePowerupSpawning();
    }

    void HandlePowerupSpawning()
    {
        _powerupSpawnTimer -= Time.deltaTime;
        if (_powerupSpawnTimer < 0)
        {
            _powerupSpawnTimer = _gameControllerInstance.powerupSpawnTimerMax;

            float EnrichedSpawnPositionX = Random.Range(10f, 13f);
            Vector2 spawnPoint = new Vector2(EnrichedSpawnPositionX, Random.Range(-4.2f, 5f));
            PowerupSpawn(spawnPoint);
        }
    }

    void PowerupSpawn(Vector2 spawnPoint)
    {
        int indexPowerups = Random.Range(0, _POWERUP_TYPES_COUNT);
        GameObject powerup;

        if (indexPowerups == 0)
            powerup = _objectPoolingManagerInstance.Get("bluePowerup");
        else if (indexPowerups == 1)
            powerup = _objectPoolingManagerInstance.Get("greenPowerup");
        else
            powerup = _objectPoolingManagerInstance.Get("redPowerup");

        powerup.transform.rotation = Quaternion.Euler(0, 0, 0);
        powerup.transform.position = spawnPoint;
        powerup.gameObject.SetActive(true);
    }
}
