using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    [SerializeField] float _delay = 3f;

    Transform _aim;
    Enemy _enemy;

    ObjectPoolingManager _objectPoolingManagerInstance;
    SoundManager _soundManagerInstance;
    GameController _gameControllerInstance;

    float _nextFireTime;

    void Awake()
    {
        _enemy = GetComponent<Enemy>();

        _objectPoolingManagerInstance = ObjectPoolingManager.Instance;
        _soundManagerInstance = SoundManager.GetInstance();
        _gameControllerInstance = GameController.GetInstance();

        _aim = transform.Find("Aim");
    }

    void Update()
    {
        if (ReadyToFire() && _gameControllerInstance.GetCurrentState() == State.Playing)
            Fire();
    }

    bool ReadyToFire() => Time.time >= _nextFireTime && _enemy.Alive; //&& transform.position.x < 9;

    void Fire()
    {
        _nextFireTime = Time.time + _delay;

        GameObject projectile = _objectPoolingManagerInstance.Get("enemyFireball");
        projectile.transform.rotation = Quaternion.Euler(0, 0, 180);
        projectile.transform.position = _aim.position;
        projectile.gameObject.SetActive(true);

        _soundManagerInstance.Play(SoundManager.SoundTags.FireballLaunch);
    }
}
