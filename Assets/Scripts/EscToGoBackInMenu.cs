using UnityEngine;
using UnityEngine.UI;

public class EscToGoBackInMenu : MonoBehaviour
{
    [SerializeField] Button _backButton;

    void OnEnable()
    {
        InputManager.Instance.OnPauseAction += InputManager_PauseAction;
    }

    void OnDisable()
    {
        InputManager.Instance.OnPauseAction -= InputManager_PauseAction;
    }

    void InputManager_PauseAction()
    {
        EscToGoBack();
    }

    public void EscToGoBack()
    {
        if(_backButton.gameObject.activeSelf)
            _backButton.onClick.Invoke();
    }
}
