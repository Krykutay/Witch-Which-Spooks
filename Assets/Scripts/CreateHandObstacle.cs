using UnityEngine;

public class CreateHandObstacle : MonoBehaviour
{
    ObjectPoolingManager _objectPoolingManagerInstance;
    GameController _gameControllerInstance;

    float _handSpawnTimer;

    const int _HAND_TYPES_COUNT = 4;

    void Awake()
    {
        _objectPoolingManagerInstance = ObjectPoolingManager.Instance;
        _gameControllerInstance = GameController.GetInstance();
    }

    void Update()
    {
        HandleHandSpawning();
    }

    void HandleHandSpawning()
    {
        _handSpawnTimer -= Time.deltaTime;
        if (_handSpawnTimer < 0)
        {
            _handSpawnTimer = _gameControllerInstance.handSpawnTimerMax;

            SpawnHand();
        }
    }

    void SpawnHand()
    {
        int indexHands = Random.Range(0, _HAND_TYPES_COUNT);
        float EnrichedSpawnPositionX = Random.Range(10f, 12f);

        GameObject hand;
        if (indexHands == 0)
            hand = _objectPoolingManagerInstance.Get("handOne");
        else if (indexHands == 1)
            hand = _objectPoolingManagerInstance.Get("handTwo");
        else if (indexHands == 2)
            hand = _objectPoolingManagerInstance.Get("handThree");
        else
            hand = _objectPoolingManagerInstance.Get("handFour");

        hand.transform.rotation = Quaternion.Euler(0, 0, 0);
        hand.transform.position = new Vector2(EnrichedSpawnPositionX, -4.95f);

        hand.gameObject.SetActive(true);
    }

}
