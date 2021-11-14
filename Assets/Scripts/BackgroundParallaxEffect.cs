using UnityEngine;

public class BackgroundParallaxEffect : MonoBehaviour
{
    [SerializeField] float _parallaxEffect = 0;

    GameController _gameControllerInstance;

    float _length;
    float _startPos;

    void Awake()
    {
        _length = GetComponent<SpriteRenderer>().bounds.size.x;

        _gameControllerInstance = GameController.GetInstance();
    }

    void Start()
    {
        _startPos = transform.position.x;
    }

    void Update()
    {
        if (_gameControllerInstance.GetCurrentState() == State.Playing)
        {
            transform.position += Vector3.left * Time.deltaTime * _gameControllerInstance.GetMoveLeftSpeed() * _parallaxEffect;

            if (transform.position.x < _startPos - _length)
                transform.position = new Vector3(_startPos, transform.position.y, transform.position.z);
        }
    }
}
