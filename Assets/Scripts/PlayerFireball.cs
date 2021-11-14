using System.Collections;
using UnityEngine;

public class PlayerFireball : MonoBehaviour
{
    [SerializeField] float _velocity = 4;

    Animator _anim;
    Collider2D _collider2d;
    Rigidbody2D _rigidbody;

    ObjectPoolingManager _objectPoolingManagerInstance;
    SoundManager _soundManagerInstance;
    GameController _gameControllerInstance;

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

        StartCoroutine(Lifetime((_gameControllerInstance.objectsRightSideDepsawnPointOnX - transform.position.x) / _initialVelocity.x));
    }

    IEnumerator Lifetime(float duration)
    {
        yield return new WaitForSeconds(duration);
        _objectPoolingManagerInstance.ReturnToPool("playerFireball", this.gameObject);
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
        _objectPoolingManagerInstance.ReturnToPool("playerFireball", this.gameObject);
    }
}
