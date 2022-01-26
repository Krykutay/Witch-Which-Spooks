using UnityEngine;

public class EnemyMoveUpAndDown : MonoBehaviour
{
    [SerializeField] float _heightVariance = 1f;

    Enemy _enemy;

    void Awake()
    {
        _enemy = GetComponent<Enemy>();
    }

    void Update()
    {
        if (_enemy.Alive)
            transform.position += Vector3.up * Mathf.Sin(Time.time) * Time.deltaTime * _heightVariance;
    }
}
