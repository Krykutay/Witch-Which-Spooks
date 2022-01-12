using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFireball : MonoBehaviour
{
    [SerializeField] float _velocity = 4;

    Animator _anim;
    Collider2D _collider2d;
    Rigidbody2D _rigidbody;

    float _lifeTime;
    float _spawnTime;

    ObjectPoolingManager _objectPoolingManagerInstance;
    SoundManager _soundManagerInstance;
    GameController _gameControllerInstance;

    static Dictionary<EnemyFireball, bool> _existingEnemyFireballs = new Dictionary<EnemyFireball, bool>();

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _collider2d = GetComponent<Collider2D>();

        _objectPoolingManagerInstance = ObjectPoolingManager.Instance;
        _soundManagerInstance = SoundManager.GetInstance();
        _gameControllerInstance = GameController.GetInstance();
    }

    void OnEnable()
    {
        _collider2d.enabled = true;

        Vector2 _initialVelocity = transform.right * (_velocity + _gameControllerInstance.GetMoveLeftSpeed() * 0.75f);
        _rigidbody.velocity = _initialVelocity;

        _existingEnemyFireballs.Add(this, true);

        _spawnTime = Time.time;
        _lifeTime = (transform.position.x - _gameControllerInstance.objectsLeftSideDepsawnPointOnX) / Mathf.Abs(_initialVelocity.x);
    }

    void OnDisable()
    {
        _existingEnemyFireballs.Remove(this);
    }

    void Update()
    {
        if (Time.time > _spawnTime + _lifeTime)
            _objectPoolingManagerInstance.ReturnToPool("enemyFireball", gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        _collider2d.enabled = false;
        _soundManagerInstance.Play(SoundManager.SoundTags.FireballHit);
        _anim.SetTrigger("Hit");
        StartCoroutine(FireballFadeOut());
    }

    IEnumerator FireballFadeOut()
    {
        yield return new WaitForSeconds(0.3f);
        _objectPoolingManagerInstance.ReturnToPool("enemyFireball", this.gameObject);
    }

    public void Die()
    {
        _objectPoolingManagerInstance.ReturnToPool("enemyFireball", this.gameObject);
    }

    public static Dictionary<EnemyFireball, bool> GetExistingEnemyFireballs()
    {
        return new Dictionary<EnemyFireball, bool>(_existingEnemyFireballs);
    }
}
