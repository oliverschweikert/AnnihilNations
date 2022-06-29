using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsVolume : MonoBehaviour
{
    public TextMeshProUGUI volumeLabel;
    public AudioMixer audioMixer;
    public void UpdateVolumeLabel(float vol) { volumeLabel.text = (vol + 80).ToString(); }
    public void SetMusicVolume(float vol) { audioMixer.SetFloat("MusicVolume", vol); }
    public void SetEffectsVolume(float vol) { audioMixer.SetFloat("EffectVolume", vol); }
}
