using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public ScriptableObject gameSettings;
    Resolution[] resolutions;
    public TMP_Dropdown resolutionDropdown;
    private void Start()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        int screenResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            options.Add($"{resolutions[i].width} x {resolutions[i].height}");
            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height) screenResolutionIndex = i;
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = screenResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }
    public void SetGraphics(int qualityIndex) { QualitySettings.SetQualityLevel(qualityIndex); }
    public void SetFullscreen(bool fullscreen) { Screen.fullScreen = fullscreen; }
    public void SetResolution(int resolutionIndex) { Screen.SetResolution(resolutions[resolutionIndex].width, resolutions[resolutionIndex].height, Screen.fullScreen); }
}
