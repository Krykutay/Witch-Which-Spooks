using UnityEngine;
using UnityEngine.UI;

public class ManaBar : MonoBehaviour
{
    RawImage barRawImage;
    float barMaskWidth;
    RectTransform barMaskRectTransform;
    RectTransform edgeRectTransform;

    GameController _gameControllerInstance;
    PlayerMana _playerManaInstance;

    float _manaNormalized;

    void Awake()
    {
        _gameControllerInstance = GameController.GetInstance();
        _playerManaInstance = PlayerMana.GetInstance();
        barMaskRectTransform = transform.Find("barMask").GetComponent<RectTransform>();
        barRawImage = transform.Find("barMask").Find("bar").GetComponent<RawImage>();
        edgeRectTransform = transform.Find("edge").GetComponent<RectTransform>();

        barMaskWidth = barMaskRectTransform.sizeDelta.x;
    }

    void Update()
    {
        if (_gameControllerInstance.currentState != State.Playing)
            return;

        _manaNormalized = _playerManaInstance.GetManaNormalized();

        Rect uvRect = barRawImage.uvRect;
        uvRect.x -= 0.2f * Time.deltaTime;
        barRawImage.uvRect = uvRect;

        Vector2 barMaskSizeDelta = barMaskRectTransform.sizeDelta;
        barMaskSizeDelta.x = _manaNormalized * barMaskWidth;
        barMaskRectTransform.sizeDelta = barMaskSizeDelta;

        edgeRectTransform.anchoredPosition = new Vector2(_manaNormalized * barMaskWidth, 0);
        edgeRectTransform.gameObject.SetActive(_manaNormalized < 1f);
        
    }
}