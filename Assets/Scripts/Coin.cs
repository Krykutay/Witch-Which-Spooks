using System;
using System.Collections;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public static event Action Collected;

    ObjectPoolingManager _objectPoolingManagerInstance;
    GameController _gameControllerInstance;
    SoundManager _soundManagerInstance;

    void Awake()
    {
        _objectPoolingManagerInstance = ObjectPoolingManager.Instance;
        _soundManagerInstance = SoundManager.GetInstance();
        _gameControllerInstance = GameController.GetInstance();
    }

    void OnEnable()
    {
        StartCoroutine(Lifetime((transform.position.x - _gameControllerInstance.objectsLeftSideDepsawnPointOnX) / _gameControllerInstance.GetMoveLeftSpeed()));
    }

    IEnumerator Lifetime(float duration)
    {
        yield return new WaitForSeconds(duration);
        _objectPoolingManagerInstance.ReturnToPool("coin", this.gameObject);
    }

    void Update()
    {
        if (_gameControllerInstance.GetCurrentState() == State.Playing)
        {
            transform.position += Vector3.left * Time.deltaTime * _gameControllerInstance.GetMoveLeftSpeed();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _soundManagerInstance.Play(SoundManager.SoundTags.CoinPickup);
            Collected?.Invoke();
            _objectPoolingManagerInstance.ReturnToPool("coin", this.gameObject);
        }
    }

}
