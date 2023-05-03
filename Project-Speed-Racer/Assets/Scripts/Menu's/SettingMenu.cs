using UnityEngine;
using UnityEngine.Audio;

// klein beetje code geleend van de breakys
// source https://www.youtube.com/watch?v=YOaYQrN1oYQ
public class SettingMenu : MonoBehaviour
{
    //audio changer
    public AudioMixer mainMixer; // allows us to use the audio mixer
    public void SetVolume(float volume)// allows to change volume
    {
        mainMixer.SetFloat("volume", volume); // sets volume
    }

    // Guality
    public void SetQuality (int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    //fullscreen
    public void setFullscreen(bool isFullscreen) // pressing the button will change fullscreen
    {
        Screen.fullScreen = isFullscreen;
        Debug.Log("FullScreen");
    }
}
