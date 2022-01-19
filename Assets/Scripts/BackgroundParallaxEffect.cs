using UnityEngine;

public class BackgroundParallaxEffect : MonoBehaviour
{
    [SerializeField] float _parallaxEffect = 0;

    GameController _gameControllerInstance;

    float _length;
    float _startPos;

    Vector3 _workSpace;

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
        if (_gameControllerInstance.currentState != State.Playing)
            return;

        transform.position += _gameControllerInstance.GetMoveLeftSpeed() * _parallaxEffect * Time.deltaTime * Vector3.left;

        if (transform.position.x < _startPos - _length)
        {
            _workSpace.Set(_startPos, transform.position.y, transform.position.z);
            transform.position = _workSpace;
        }
    }
}
