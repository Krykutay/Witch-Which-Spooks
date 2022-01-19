using System;
using System.Collections;
using UnityEngine;

public class PowerupManagement : MonoBehaviour
{
    public event Action BluePotionActivated;
    public event Action BluePotionDeplated;
    public event Action RedPotionActivated;

    [SerializeField] GameObject _bluePotionCountdown;
    [SerializeField] GameObject _greenPotionCountdown;
    [SerializeField] GameObject _redPotionCountdown;

    SpriteRenderer _spriteRenderer;
    GameObject _blueSparkles;
    GameObject _playerCollider;
    PowerupCountdownBar _bluePotionCountdownBar;
    PowerupCountdownBar _greenPotionCountdownBar;
    PowerupCountdownBar _redPotionCountdownBar;

    IEnumerator _deactivateBluePowerup;
    IEnumerator _deactivateGreenPowerup;
    IEnumerator _deactivateRedPowerup;

    Color _color;
    Color _greenPotionPlayerColor;
    Color _redPotionPlayerColor;

    Vector3 _blueSparkleOrigin;
    bool _isBluePowerupActive;

    static PowerupManagement _instance;

    public static PowerupManagement GetInstance()
    {
        return _instance;
    }

    void Awake()
    {
        _instance = this;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _blueSparkles = transform.Find("BlueSparkles").gameObject;
        _bluePotionCountdownBar = _bluePotionCountdown.GetComponent<PowerupCountdownBar>();
        _greenPotionCountdownBar = _greenPotionCountdown.GetComponent<PowerupCountdownBar>();
        _redPotionCountdownBar = _redPotionCountdown.GetComponent<PowerupCountdownBar>();
        _playerCollider = transform.Find("collider").gameObject;
    }

    void OnEnable()
    {
        Powerup.Collected += Powerup_Collected;
        _color = _spriteRenderer.color;
        _greenPotionPlayerColor = _color;
        _greenPotionPlayerColor.a = 0.35f;
        _redPotionPlayerColor = new Color(200f, 0f, 0f, 255f);
    }

    void Start()
    {
        _blueSparkleOrigin = _blueSparkles.transform.localPosition;
        _blueSparkles.SetActive(false);
        _bluePotionCountdownBar.gameObject.SetActive(false);
        _greenPotionCountdownBar.gameObject.SetActive(false);
        _redPotionCountdownBar.gameObject.SetActive(false);
    }

    void OnDisable()
    {
        Powerup.Collected -= Powerup_Collected;
    }

    void Powerup_Collected(Powerup powerup)
    {
        if (powerup.GetIsBlue())
        {
            _bluePotionCountdownBar.gameObject.SetActive(true);
            _bluePotionCountdownBar.StartCountdown(powerup.GetDuration());
            BluePotionActivated?.Invoke();
            _blueSparkles.SetActive(true);
            _isBluePowerupActive = true;

            if (_deactivateBluePowerup != null)
                StopCoroutine(_deactivateBluePowerup);
            _deactivateBluePowerup = DeactivateBluePowerup(powerup);
            StartCoroutine(_deactivateBluePowerup);
        }
        else if (powerup.GetIsGreen())
        {
            _greenPotionCountdownBar.gameObject.SetActive(true);
            _greenPotionCountdownBar.StartCountdown(powerup.GetDuration());
            _spriteRenderer.color = _greenPotionPlayerColor;
            gameObject.layer = 12;  // A layer where player doesn't get hit by hostile effects
            _playerCollider.layer = 12;

            if (_deactivateGreenPowerup != null)
                StopCoroutine(_deactivateGreenPowerup);
            _deactivateGreenPowerup = DeactivateGreenPowerup(powerup);
            StartCoroutine(_deactivateGreenPowerup);
        }
        else // if Red
        {
            _redPotionCountdownBar.gameObject.SetActive(true);
            _redPotionCountdownBar.StartCountdown(powerup.GetDuration());
            _spriteRenderer.color = _redPotionPlayerColor;
            RedPotionActivated?.Invoke();

            if (_deactivateRedPowerup != null)
                StopCoroutine(_deactivateRedPowerup);
            _deactivateRedPowerup = DeactivateRedPowerup(powerup);
            StartCoroutine(_deactivateRedPowerup);
        }
    }

    IEnumerator DeactivateBluePowerup(Powerup powerup)
    {
        yield return new WaitForSeconds(powerup.GetDuration());
        BluePotionDeplated?.Invoke();
        _bluePotionCountdownBar.gameObject.SetActive(false);
        _blueSparkles.transform.localPosition = _blueSparkleOrigin;
        _blueSparkles.SetActive(false);
        _isBluePowerupActive = false;
        _deactivateBluePowerup = null;
    }

    IEnumerator DeactivateGreenPowerup(Powerup powerup)
    {
        yield return new WaitForSeconds(powerup.GetDuration());
        _greenPotionCountdownBar.gameObject.SetActive(false);

        if (_deactivateRedPowerup != null)
            _spriteRenderer.color = _redPotionPlayerColor;
        else
            _spriteRenderer.color = _color;

        gameObject.layer = 8;   // Back to Player layer
        _playerCollider.layer = 8;
        _deactivateGreenPowerup = null;
    }

    IEnumerator DeactivateRedPowerup(Powerup powerup)
    {
        yield return new WaitForSeconds(powerup.GetDuration());
        _redPotionCountdownBar.gameObject.SetActive(false);

        if (_deactivateGreenPowerup != null)
            _spriteRenderer.color = _greenPotionPlayerColor;
        else
            _spriteRenderer.color = _color;

        _deactivateRedPowerup = null;
    }

    public bool GetIsBluePowerupActive()
    {
        return _isBluePowerupActive;
    }
}
