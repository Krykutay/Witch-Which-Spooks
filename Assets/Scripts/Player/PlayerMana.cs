using UnityEngine;

public class PlayerMana : MonoBehaviour
{
    const int _MANA_MAX = 100;

    float _manaAmount;
    float _manaRegenAmount = 25f;

    GameController _gameControllerInstance;

    static PlayerMana _instance;

    public static PlayerMana GetInstance()
    {
        return _instance;
    }

    void Awake()
    {
        _instance = this;
        _gameControllerInstance = GameController.GetInstance();
    }

    void OnDisable()
    {
        PowerupManagement.GetInstance().BluePotionActivated -= PowerupManagement_BluePotionActivated;
        PowerupManagement.GetInstance().BluePotionDeplated -= PowerupManagement_BluePotionDeplated;
    }

    void Start()
    {
        PowerupManagement.GetInstance().BluePotionActivated += PowerupManagement_BluePotionActivated;
        PowerupManagement.GetInstance().BluePotionDeplated += PowerupManagement_BluePotionDeplated;
        _manaAmount = _MANA_MAX;
    }

    void Update()
    {
        if (_gameControllerInstance.currentState == State.Playing)
        {
            _manaAmount += _manaRegenAmount * Time.deltaTime;
            _manaAmount = Mathf.Clamp(_manaAmount, 0f, _MANA_MAX);
        }
    }

    public void TrySpendMana(int amount)
    {
        if (_manaAmount >= amount)
            _manaAmount -= amount;
    }

    void PowerupManagement_BluePotionActivated()
    {
        _manaAmount = _MANA_MAX;
        _manaRegenAmount = 40f;
    }

    void PowerupManagement_BluePotionDeplated()
    {
        _manaRegenAmount = 25f;
    }

    public float GetManaNormalized() => _manaAmount / _MANA_MAX;

    public float GetManaAmount() => _manaAmount;
}
