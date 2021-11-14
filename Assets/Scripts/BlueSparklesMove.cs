using UnityEngine;

public class BlueSparklesMove : MonoBehaviour
{
    [SerializeField] float _distanceVariance = 0.5f;
    [SerializeField] float _frequency = 2f;

    GameController _gameControllerInstance;

    void Awake()
    {
        _gameControllerInstance = GameController.GetInstance();
    }

    void Update()
    {
        if (_gameControllerInstance.GetCurrentState() == State.Playing)
        {
            transform.position += Vector3.up * Mathf.Sin(Time.time * _frequency) * Time.deltaTime * _distanceVariance +
                Vector3.right * Mathf.Cos(Time.time * _frequency) * Time.deltaTime * _distanceVariance;
        }
    }
}
