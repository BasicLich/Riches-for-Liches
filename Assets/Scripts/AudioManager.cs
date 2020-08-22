using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [System.Serializable]
    public class Sound
    {
        public string name;

        public AudioClip clip;

        [Range(0.0f, 1.0f)]
        public float volume;
        [Range(0.1f, 3.0f)]
        public float pitch;
        public bool loop;

        [HideInInspector]
        public AudioSource source;
    }

    public static AudioManager instance;

    public Sound[] sounds;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound _s in sounds)
        {
            _s.source = gameObject.AddComponent<AudioSource>();
            _s.source.clip = _s.clip;
            _s.source.volume = _s.volume;
            _s.source.pitch = _s.pitch;
            _s.source.loop = _s.loop;
        }
    }

    public void Play(string _name)
    {
        Sound _s = Array.Find(sounds, sound => sound.name == _name);
        if (_s == null)
        {
            Debug.LogWarning("Sound " + _s.name + " is missing!");
            return;
        }
        _s.source.Play();
    }

    public void Stop(string _name)
    {
        Sound _s = Array.Find(sounds, sound => sound.name == _name);
        if (_s == null)
        {
            Debug.LogWarning("Sound " + _s.name + " is missing!");
            return;
        }
        _s.source.Stop();
    }
}
