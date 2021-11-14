using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HandObstacleHealthBar : MonoBehaviour
{
    RawImage barRawImage;
    float barMaskWidth;
    RectTransform barMaskRectTransform;
    RectTransform edgeRectTransform;

    GameController _gameControllerInstance;

    float _maxHealth;
    float _currentHealth;

    void Awake()
    {
        barMaskRectTransform = transform.Find("barMask").GetComponent<RectTransform>();
        barRawImage = transform.Find("barMask").Find("bar").GetComponent<RawImage>();
        edgeRectTransform = transform.Find("edge").GetComponent<RectTransform>();

        barMaskWidth = barMaskRectTransform.sizeDelta.x;

        _gameControllerInstance = GameController.GetInstance();
    }

    void Update()
    {
        if (_gameControllerInstance.GetCurrentState() == State.Playing)
        {
            Rect uvRect = barRawImage.uvRect;
            uvRect.x -= 0.2f * Time.deltaTime;
            barRawImage.uvRect = uvRect;

            Vector2 barMaskSizeDelta = barMaskRectTransform.sizeDelta;
            barMaskSizeDelta.x = HealthNormalized() * barMaskWidth;
            barMaskRectTransform.sizeDelta = barMaskSizeDelta;

            edgeRectTransform.anchoredPosition = new Vector2(HealthNormalized() * barMaskWidth, 0);

            edgeRectTransform.gameObject.SetActive(HealthNormalized() < 1f);
        }
    }

    float HealthNormalized() => _currentHealth / _maxHealth;

    public void SetMaxHealth(int maxHealth) => _maxHealth = (float)maxHealth;

    public void SetCurrentHealth(int currentHealth)
    {
        StartCoroutine(DropHealthSmoothly(currentHealth));
    }

    IEnumerator DropHealthSmoothly(int healthAfterHit)
    {
        while (healthAfterHit < _currentHealth)
        {
            _currentHealth -= 0.04f;
            yield return new WaitForSeconds(0.01f);
        }
        _currentHealth = healthAfterHit;
    }
}
