using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandObstacles : MonoBehaviour
{
    [SerializeField] int _health = 2;
    [SerializeField] string _handPoolKey;

    HandObstacleHealthBar _healthbar;
    Transform _canvas;
    Transform _collider;
    Coroutine _disableHealthbar;

    Vector2 _handInitialScale;
    Vector2 _healthbarInitialScale;

    float _lifeTime;
    float _spawnTime;

    ObjectPoolingManager _objectPoolingManagerInstance;
    GameController _gameControllerInstance;

    static Dictionary<HandObstacles, Transform> _existingHands = new Dictionary<HandObstacles, Transform>();

    int _currentHealth;
    bool _objectSpawned;

    void Awake()
    {
        _healthbar = transform.Find("Canvas").Find("HealthBar").GetComponent<HandObstacleHealthBar>();
        _canvas = transform.Find("Canvas");
        _collider = transform.Find("collider");

        _handInitialScale = transform.localScale;
        _healthbarInitialScale = _healthbar.transform.localScale;

        _objectPoolingManagerInstance = ObjectPoolingManager.Instance;
        _gameControllerInstance = GameController.GetInstance();
        _objectSpawned = false;
    }

    void OnEnable()
    {
        _currentHealth = _health;
        _canvas.gameObject.SetActive(true);
        _healthbar.SetMaxHealth(_health);
        _healthbar.SetCurrentHealth(_health, 0);
        _canvas.gameObject.SetActive(false);

        if (!_existingHands.ContainsKey(this))
            _existingHands.Add(this, _collider);

        _spawnTime = Time.time;
        _lifeTime = (transform.position.x - _gameControllerInstance.objectsLeftSideDepsawnPointOnX) / _gameControllerInstance.GetMoveLeftSpeed();

        if (_objectSpawned)
        {
            ScaleHandsAndHealthbar();
        }
    }

    void OnDisable()
    {
        _existingHands.Remove(this);
    }

    void Start()
    {
        _objectSpawned = true;
        ScaleHandsAndHealthbar();
    }

    void Update()
    {
        if (Time.time > _spawnTime + _lifeTime)
        {
            ScaleBackToNormal();
            _objectPoolingManagerInstance.ReturnToPool(_handPoolKey, gameObject);
        }

        transform.position += _gameControllerInstance.GetMoveLeftSpeed() * Time.deltaTime * Vector3.left;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("PlayerFireball"))
        {
            _currentHealth--;
            _canvas.gameObject.SetActive(true);

            if (_disableHealthbar != null)
                StopCoroutine(DisableHealthBar());
            _disableHealthbar = StartCoroutine(DisableHealthBar());

            _healthbar.SetCurrentHealth(_currentHealth, 1);
            if (_currentHealth <= 0)
                Die();
        }
    }

    IEnumerator DisableHealthBar()
    {
        yield return new WaitForSeconds(2f);
        _canvas.gameObject.SetActive(false);
    }

    public void Die()
    {
        ScoreManager.GetInstance().HandObstacle_Died();
        ScaleBackToNormal();
        _objectPoolingManagerInstance.ReturnToPool(_handPoolKey, gameObject);
    }

    void ScaleHandsAndHealthbar()
    {
        float handScaleOnY = UnityEngine.Random.Range(1f, 2f);
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y * handScaleOnY, transform.localScale.z);

        Transform healthBar = _healthbar.transform;
        healthBar.localScale = new Vector3(
            healthBar.localScale.x * (1f + (handScaleOnY - 1) / 2.25f)
            , healthBar.localScale.y * (1f + (handScaleOnY - 1) / 2.25f) / handScaleOnY,
            healthBar.localScale.z);
    }

    void ScaleBackToNormal()
    {
        transform.localScale = _handInitialScale;
        _healthbar.transform.localScale = _healthbarInitialScale;
    }

    public static Dictionary<HandObstacles, Transform> GetExistingHandObstacles()
    {
        return new Dictionary<HandObstacles, Transform>(_existingHands);
    }

}
