using UnityEngine;
using UnityEngine.UI;

public class PowerupCountdownBar : MonoBehaviour
{
    Image _barImage;

    GameController _gameControllerInstance;

    float _duration;
    float _remaningDuration;

    void Awake()
    {
        _barImage = transform.Find("bar").GetComponent<Image>();

        _gameControllerInstance = GameController.GetInstance();
    }

    void Update()
    {
        if (_gameControllerInstance.currentState != State.Playing)
            return;

        if (_remaningDuration <= 0)
        {
            return;
        }
        _remaningDuration -= Time.deltaTime;
        _barImage.fillAmount = GetDurationNormalized();
        
    }
    
    float GetDurationNormalized()
    {
        return _remaningDuration / _duration;
    }

    public void StartCountdown(float duration)
    {
        _duration = duration;
        _remaningDuration = duration;
    }
}
