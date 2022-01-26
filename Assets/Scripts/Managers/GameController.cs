using UnityEngine;
using UnityEngine.InputSystem;

public enum State
{
    Playing,
    PlayerStop,
}

public class GameController : MonoBehaviour
{
    [SerializeField] GameObject _menu;
    [SerializeField] GameObject _gameoverPanel;

    [SerializeField] float _difficultyTimerMax = 2.5f;
    [SerializeField] float _moveLeftSpeed = 4f;
    [SerializeField] float _initiaMovelLeftSpeed = 4f;

    [SerializeField] float _handSpawnTimerMax = 3f;
    [SerializeField] float _powerupSpawnTimerMax = 10f;
    [SerializeField] float _coinSpawnTimerMax = 4.5f;
    [SerializeField] float _enemySpawnTimerMax = 5.8f;

    public State currentState { get; private set; }
    public float initialMoveLeftSpeed { get { return _initiaMovelLeftSpeed; } }
    public float handSpawnTimerMax { get { return _handSpawnTimerMax; } }
    public float powerupSpawnTimerMax { get { return _powerupSpawnTimerMax; } }
    public float coinSpawnTimerMax { get { return _coinSpawnTimerMax; } }
    public float enemySpawnTimerMax { get { return _enemySpawnTimerMax; } }
    public float objectsLeftSideDepsawnPointOnX { get { return _objectsLeftSideDepsawnPointOnX; } }
    public float objectsRightSideDepsawnPointOnX { get { return _objectsRightSideDepsawnPointOnX; } }

    float _objectsLeftSideDepsawnPointOnX = -11f;
    float _objectsRightSideDepsawnPointOnX = 11f;
    float _difficultyTimer;
    int _maxDifficulty;

    SoundManager _soundManagerInstance;

    static GameController _instance;
    
    public static GameController GetInstance()
    {
        return _instance;
    }

    void Awake()
    {
        _instance = this;

        currentState = State.Playing;

        _soundManagerInstance = SoundManager.GetInstance();
    }

    void Start()
    {
        InputManager.Instance.OnPauseAction += InputManager_PauseAction;
        WitchKill.GetInstance().PlayerDied += WitchKill_PlayerDied;
    }

    void OnDisable()
    {
        InputManager.Instance.OnPauseAction -= InputManager_PauseAction;
        WitchKill.GetInstance().PlayerDied -= WitchKill_PlayerDied;
    }

    void Update()
    {
        if (currentState != State.Playing)
            return;

        _difficultyTimer -= Time.deltaTime;
        if (_difficultyTimer < 0)
        {
            _difficultyTimer = _difficultyTimerMax;
            SetDifficulty();
        }
    }

    void InputManager_PauseAction()
    {
        if (currentState == State.Playing)
            Game_Paused();
    }

    void WitchKill_PlayerDied()
    {
        //StopAllCoroutines();
        Time.timeScale = 0f;
        _gameoverPanel.SetActive(true);
        _soundManagerInstance.Stop(SoundManager.SoundTags.Ambient);
        _soundManagerInstance.Play(SoundManager.SoundTags.Gameover);
        currentState = State.PlayerStop;
    }

    public void Game_Resumed()
    {
        Time.timeScale = 1f;
        currentState = State.Playing;
    }

    public void Game_Paused()
    {
        if (currentState == State.Playing)
        {
            _menu.gameObject.SetActive(true);
            Time.timeScale = 0f;
            currentState = State.PlayerStop;
        }
    }

    void SetDifficulty()
    {
        if (_maxDifficulty < 60)
        {
            _maxDifficulty++;
            _moveLeftSpeed += 0.1f;
            _handSpawnTimerMax -= 0.035f;
            _powerupSpawnTimerMax -= 0.1f;
            _coinSpawnTimerMax -= 0.05f;
            _enemySpawnTimerMax -= 0.05f;
        }
    }

    public float GetMoveLeftSpeed()
    {
        return _moveLeftSpeed;
    }
         
}

