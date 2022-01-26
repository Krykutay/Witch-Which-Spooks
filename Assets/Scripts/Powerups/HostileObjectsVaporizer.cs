using System.Collections.Generic;
using UnityEngine;

public class HostileObjectsVaporizer : MonoBehaviour
{
    ObjectPoolingManager _objectPoolingManagerInstance;

    void Awake()
    {
        _objectPoolingManagerInstance = ObjectPoolingManager.Instance;
    }
    void Start()
    {
        PowerupManagement.GetInstance().RedPotionActivated += PowerupManagement_RedPotionActivated;
    }

    void OnDisable()
    {
        PowerupManagement.GetInstance().RedPotionActivated -= PowerupManagement_RedPotionActivated;
    }

    void SpawnParticleEffect(Vector2 transformPosition, Quaternion rotation , Vector3 localScale)
    {
        GameObject particleEffect = _objectPoolingManagerInstance.Get("particleEffect");

        particleEffect.transform.rotation = rotation;
        particleEffect.transform.position = transformPosition;
        particleEffect.transform.localScale = new Vector3(localScale.x, localScale.y, localScale.z);

        particleEffect.gameObject.SetActive(true);
    }

    void PowerupManagement_RedPotionActivated()
    {
        Dictionary<Enemy, bool> tempEnemies = Enemy.GetExistingEnemies();
        Dictionary<HandObstacles, Transform> tempHandObstacles = HandObstacles.GetExistingHandObstacles();
        Dictionary<EnemyFireball, bool> tempEnemyFireballs = EnemyFireball.GetExistingEnemyFireballs();

        foreach (Enemy enemy in tempEnemies.Keys)
        {
            SpawnParticleEffect(enemy.transform.position, Quaternion.Euler(0, 0, 0), new Vector3(1, 1, 1));
            enemy.Die(true);
        }

        foreach (HandObstacles handObstacle in tempHandObstacles.Keys)
        {
            Vector3 localScale = handObstacle.transform.localScale / 1.3f;
            Vector3 position = tempHandObstacles[handObstacle].position;
            SpawnParticleEffect(position, Quaternion.Euler(0, 0, 0), localScale);
            handObstacle.Die();
        }

        foreach (EnemyFireball enemyFireball in tempEnemyFireballs.Keys)
        {
            Vector3 localScale = enemyFireball.transform.localScale;
            SpawnParticleEffect(enemyFireball.transform.position, Quaternion.Euler(0, 0, 0), localScale);
            enemyFireball.Die();
        }
    }

}
