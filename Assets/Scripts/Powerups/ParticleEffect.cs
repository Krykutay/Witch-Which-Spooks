using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEffect : MonoBehaviour
{
    ParticleSystem _particleEffect;
    ObjectPoolingManager _objectPoolingManagerInstance;
    GameController _gameControllerInstance;

    float _particleEffectDuration;
    bool _objectSpawned;

    Vector2 _particleEffectInitialScale;

    void Awake()
    {
        _particleEffect = GetComponent<ParticleSystem>();

        _objectPoolingManagerInstance = ObjectPoolingManager.Instance;
        _gameControllerInstance = GameController.GetInstance();

        _particleEffectInitialScale = transform.localScale;
        _objectSpawned = false;
    }

    void OnEnable()
    {
        _particleEffectDuration = _particleEffect.main.duration;
        if (_objectSpawned)
        {
            if (_particleEffect.isPlaying)
                _particleEffect.Stop();

            _particleEffect.Play();
            StartCoroutine(WaitForParticleEffect(_particleEffectDuration));
        }
    }

    void Start()
    {
        _objectSpawned = true;
        if (_particleEffect.isPlaying)
            _particleEffect.Stop();
        _particleEffect.Play();
        StartCoroutine(WaitForParticleEffect(_particleEffectDuration));
    }

    void Update()
    {
        if (_gameControllerInstance.currentState == State.Playing)
        {
            transform.position += Vector3.left * Time.deltaTime * _gameControllerInstance.GetMoveLeftSpeed()/4;
        }
    }

    IEnumerator WaitForParticleEffect(float duration)
    {
        yield return new WaitForSeconds(duration);
        transform.localScale = _particleEffectInitialScale;
        _objectPoolingManagerInstance.ReturnToPool("particleEffect", this.gameObject);
    }

}
