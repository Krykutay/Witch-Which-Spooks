using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static event Action Died;
    public static event Action<Enemy> SpawnCoinOnKilled;
    public static event Action<Enemy> SpawnPowerupOnKilled;

    static Dictionary<Enemy, bool> _existingEnemies = new Dictionary<Enemy, bool>();

    [SerializeField] int _health = 2;

    float _initialMoveSpeed;
    Vector3 _destinationPoint;
    int _currentHealth;

    Animator _anim;
    Collider2D _collider;
    Rigidbody2D _rigidBody;
    EnemyHealthBar _healthbar;

    ObjectPoolingManager _objectPoolingManagerInstance;
    GameController _gameControllerInstance;

    public bool Alive => gameObject.activeSelf && _currentHealth > 0;

    void Awake()
    {
        _anim = GetComponent<Animator>();
        _collider = GetComponent<Collider2D>();
        _rigidBody = GetComponent<Rigidbody2D>();
        _healthbar = transform.Find("Canvas").Find("HealthBar").GetComponent<EnemyHealthBar>();

        _objectPoolingManagerInstance = ObjectPoolingManager.Instance;
        _gameControllerInstance = GameController.GetInstance();

        _initialMoveSpeed = _gameControllerInstance.initialMoveLeftSpeed;
    }

    void OnEnable()
    {
        _currentHealth = _health;
        _healthbar.gameObject.SetActive(true);
        _collider.enabled = true;
        _rigidBody.bodyType = RigidbodyType2D.Kinematic;
        _rigidBody.gravityScale = 0f;


        Vector3 destinationPoint = new Vector3(UnityEngine.Random.Range(2f, 8f), transform.position.y, 0);
        SetDestination(destinationPoint);

        _healthbar.SetMaxHealth(_health);
        _healthbar.SetCurrentHealth(_health);

        if (!_existingEnemies.ContainsKey(this))
            _existingEnemies.Add(this, true);
    }

    void OnDisable()
    {
        _existingEnemies.Remove(this);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("PlayerFireball"))
        {
            _currentHealth--;
            _healthbar.SetCurrentHealth(_currentHealth);
            if (_currentHealth <= 0)
                Die(false);
            else
                _anim.SetTrigger("Hurt");
        }
    }

    public void Die(bool isInstant)
    {
        _healthbar.gameObject.SetActive(false);
        _collider.enabled = false;

        SpawnCollectableOnKilled();
        Died?.Invoke();

        if (isInstant)
        {
            _objectPoolingManagerInstance.ReturnToPool("enemy", this.gameObject);
            return;
        }

        _rigidBody.bodyType = RigidbodyType2D.Dynamic;
        _rigidBody.gravityScale = 1f;
        _anim.SetTrigger("Died");
        StartCoroutine(EnemyFadeOut());
    }

    IEnumerator EnemyFadeOut()
    {
        yield return new WaitForSeconds(3f);

        _objectPoolingManagerInstance.ReturnToPool("enemy", this.gameObject);
    }

    IEnumerator StopWhenReachDestination()
    {
        while (Mathf.Abs(transform.position.x - _destinationPoint.x) > 0.1f && Alive && _gameControllerInstance.GetCurrentState() == State.Playing)
        {
            if (transform.position.x > _destinationPoint.x)
                transform.position = Vector3.MoveTowards(transform.position, _destinationPoint, _gameControllerInstance.GetMoveLeftSpeed() * Time.deltaTime);
            else
                transform.position = Vector3.MoveTowards(transform.position, _destinationPoint, _initialMoveSpeed * Time.deltaTime);

            yield return null;
        }
    }

    public void SetDestination(Vector3 destinationPoint)
    {
        _destinationPoint = destinationPoint;
        StartCoroutine(StopWhenReachDestination());
    }

    void SpawnCollectableOnKilled()
    {
        int spawnPickupPossibility = UnityEngine.Random.Range(0, 100);
        if (spawnPickupPossibility >= 75)
            SpawnCoinOnKilled?.Invoke(this);
        else if (spawnPickupPossibility >= 50)
            SpawnPowerupOnKilled?.Invoke(this);
    }

    public static Dictionary<Enemy, bool> GetExistingEnemies()
    {
        return new Dictionary<Enemy, bool>(_existingEnemies);
    }
}
