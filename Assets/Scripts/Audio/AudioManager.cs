using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public Sound[] sounds;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.outputAudioMixerGroup = s.mixerGroup;
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.playOnAwake = s.playOnAwake;
        }
    }
    private void Start() { Play("BackgroundMusic"); }
    public void ButtonClick() { Play("ButtonClick"); }
    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning($"The following sound name was not found: {name}");
            return;
        }
        s.source.Play();
    }
    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning($"The following sound name was not found: {name}");
            return;
        }
        if (s.source.isPlaying)
            s.source.Stop();
    }
    public IEnumerator FadeIn(string name, float fadeDuration)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning($"The following sound name was not found: {name}");
        }
        else
        {
            s.source.Play();
            for (s.source.volume = 0; s.source.volume < s.volume; s.source.volume += Time.unscaledDeltaTime / fadeDuration) yield return null;
        }
    }
    public IEnumerator FadeOut(string name, float fadeDuration)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning($"The following sound name was not found: {name}");
        }
        else
        {
            if (s.source.isPlaying)
                for (; s.source.volume > 0; s.source.volume -= Time.unscaledDeltaTime / fadeDuration) yield return null;
            Stop(name);
            s.source.volume = s.volume;
        }
    }
}
