using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsController : MonoBehaviour
{
    public AudioMixer mixer;
    public UnityEngine.UI.Slider generalSlider;
    public UnityEngine.UI.Slider musicSlider;
    public UnityEngine.UI.Slider soundsSlider;

    private const string sounds = "SoundsVolume";
    private const string master = "MasterVolume";
    private const string music = "MusicVolume";

    void OnEnable()
    {
        float temp;
        mixer.GetFloat(master, out temp);
        generalSlider.value = GetSliderValue(temp);
        mixer.GetFloat(sounds, out temp);
        soundsSlider.value = GetSliderValue(temp);
        mixer.GetFloat(music, out temp);
        musicSlider.value = GetSliderValue(temp);
    }

    public void OnGeneralSliderValueChange(float value)
    {
        ApplySliderValue(value, master);
    }

    public void OnSoundsSliderValueChange(float value)
    {
        ApplySliderValue(value, sounds);
    }

    public void OnMusicSliderValueChange(float value)
    {
        ApplySliderValue(value, music);
    }

    private void ApplySliderValue(float value, string name)
    {
        float volume = GetVolume(value);
        mixer.SetFloat(name, volume);
        PlayerPrefs.SetFloat(name, volume);
    }

    private float GetSliderValue(float volume)
    {
        return Mathf.Pow(10, Mathf.Clamp(volume, -80, 20) / 20);
    }

    private float GetVolume(float sliderValue)
    {
        return Mathf.Log10(Mathf.Clamp(sliderValue, 0.0001f, 10f)) * 20;
    }
}
