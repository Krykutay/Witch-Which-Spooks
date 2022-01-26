using System;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager _instance;

    public Sound[] sounds;
    private static Dictionary<SoundTags, float> soundTimerDictionary;

    public enum SoundTags
    {
        PlayerJump,
        CoinPickup,
        PotionPickup,
        FireballLaunch,
        FireballHit,
        Ambient,
        ButtonClick,
        Gameover,
        ScoreCalculation,
    }

    public enum SoundTypes
    {
        Effect,
        Music,
    }

    public static SoundManager GetInstance()
    {
        return _instance;
    }


    void Awake()
    {
        _instance = this;

        soundTimerDictionary = new Dictionary<SoundTags, float>();

        foreach (Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound._defaultMaxVolume = sound.volume;

            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.isLoop;
            sound.source.playOnAwake = false;

            if (sound.hasCooldown)
            {
                soundTimerDictionary[sound.name] = 0.15f;
            }
        }
    }

    void Start()
    {
        Play(SoundManager.SoundTags.Ambient);
    }

    public void Play(SoundTags name)
    {
        Sound sound = Array.Find(sounds, s => s.name == name);

        if (sound == null)
        {
            Debug.LogError("Sound " + name + " Not Found!");
            return;
        }

        if (!CanPlaySound(sound)) return;

        sound.source.Play();
    }

    public void Stop(SoundTags name)
    {
        Sound sound = Array.Find(sounds, s => s.name == name);

        if (sound == null)
        {
            Debug.LogError("Sound " + name + " Not Found!");
            return;
        }

        sound.source.Stop();
    }

    private static bool CanPlaySound(Sound sound)
    {
        if (soundTimerDictionary.ContainsKey(sound.name))
        {
            float lastTimePlayed = soundTimerDictionary[sound.name];

            if (lastTimePlayed + (sound.clip.length)/8 < Time.unscaledTime)
            {
                soundTimerDictionary[sound.name] = Time.unscaledTime;
                return true;
            }
            return false;
        }
        return true;
    }

}