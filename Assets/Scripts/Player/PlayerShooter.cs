using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooter : MonoBehaviour
{
    Transform _aim;
    PowerupManagement _powerupManagement;
    PlayerInput _playerInput;
    InputAction _fireAction;

    ObjectPoolingManager _objectPoolingManagerInstance;
    SoundManager _soundManagerInstance;
    GameController _gameControllerInstance;
    PlayerMana _playerManaInstance;

    void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        _fireAction = _playerInput.actions["Fire"];

        _aim = transform.Find("Aim");
        _powerupManagement = GetComponent<PowerupManagement>();

        _objectPoolingManagerInstance = ObjectPoolingManager.Instance;
        _soundManagerInstance = SoundManager.GetInstance();
        _gameControllerInstance = GameController.GetInstance();
        _playerManaInstance = PlayerMana.GetInstance();
    }

    void OnEnable()
    {
        _fireAction.started += Fire;
    }

    void OnDisable()
    {
        _fireAction.started -= Fire;
    }

    bool isManaEnough()
    {
        float manaAmount = _playerManaInstance.GetManaAmount();
        return manaAmount > 25 || (_powerupManagement.GetIsBluePowerupActive() && manaAmount > 15);
    }

    void Fire(InputAction.CallbackContext context)
    {
        if (isManaEnough() && _gameControllerInstance.currentState != State.Playing)
            return;
        
        GameObject projectile = _objectPoolingManagerInstance.Get("playerFireball");
        projectile.transform.rotation = Quaternion.Euler(0, 0, 0);
        projectile.transform.position = _aim.position;
        projectile.gameObject.SetActive(true);

        if (!_powerupManagement.GetIsBluePowerupActive())
            _playerManaInstance.TrySpendMana(25);
        else
            _playerManaInstance.TrySpendMana(15);

        _soundManagerInstance.Play(SoundManager.SoundTags.FireballLaunch);
        
    }
}
