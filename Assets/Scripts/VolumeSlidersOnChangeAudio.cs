using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlidersOnChangeAudio : MonoBehaviour
{
    [SerializeField] GameObject _soundDialogMenu;
    [SerializeField] Slider _masterVolumeSlider;
    [SerializeField] Slider _effectVolumeSlider;
    [SerializeField] Slider _musicVolumeSlider;

    SoundManager _soundManagerInstance;

    void Awake()
    {
        _soundManagerInstance = SoundManager.GetInstance();
    }

    void Start()
    {
        _masterVolumeSlider.onValueChanged.AddListener( _=>
        {
            if (_soundDialogMenu.activeSelf)
                PlayAudio();
        });

        _effectVolumeSlider.onValueChanged.AddListener(_ =>
        {
            if (_soundDialogMenu.activeSelf)
                PlayAudio();
        });

        _musicVolumeSlider.onValueChanged.AddListener(_ =>
        {
            if (_soundDialogMenu.activeSelf)
                PlayAudio();
        });
    }

    void PlayAudio()
    {
        _soundManagerInstance.Play(SoundManager.SoundTags.ButtonClick);
    }

}
