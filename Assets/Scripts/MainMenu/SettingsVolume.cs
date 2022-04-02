using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsVolume : MonoBehaviour
{
    public TextMeshProUGUI volumeLabel;
    public Slider volumeControl;
    public void UpdateVolumeLabel()
    {
        volumeLabel.text = volumeControl.value.ToString();
    }
}
