using System;
using UnityEngine;

public class GameAudio : MonoBehaviour
{
    public AudioManager audioManager;
    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        Sound bossMusic = Array.Find(audioManager.sounds, sound => sound.name == "BossMusic");
        Sound backgroundMusic = Array.Find(audioManager.sounds, sound => sound.name == "BackgroundMusic");
        if (bossMusic.source.isPlaying)
        {
            StartCoroutine(audioManager.FadeOut("BossMusic", 5f));
            StartCoroutine(audioManager.FadeIn("BackgroundMusic", 5f));
        }
    }
    public void AudioButtonClick() { audioManager.ButtonClick(); }
}
