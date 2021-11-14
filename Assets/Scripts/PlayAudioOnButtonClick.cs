using UnityEngine;
using UnityEngine.UI;

public class PlayAudioOnButtonClick : MonoBehaviour
{
    Button _button;

    SoundManager _soundManagerInstance;

    void Awake()
    {
        _soundManagerInstance = SoundManager.GetInstance();
        _button = GetComponent<Button>();
    }

    void Start()
    {
        _button.onClick.AddListener(() =>
        {
            _soundManagerInstance.Play(SoundManager.SoundTags.ButtonClick);
        });
    }
}
