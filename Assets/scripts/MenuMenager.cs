using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MenuMenager : MonoBehaviour
{
    public GameObject menuCanvas;
    public GameObject loadingCnavas;
    public GameObject creditsCnavas;
    public GameObject settingsCanvas;
    public GameObject controlsCanvas;
    public float controlsDisplayTime;
    public AudioMixer mixer;
    public UnityEngine.UI.Slider loadingBar;

    private readonly string[] prefsNames = { "MasterVolume", "SoundsVolume", "MusicVolume" };

    private void Start()
    {
        foreach(string name in prefsNames)
        {
            if (PlayerPrefs.HasKey(name))
            {
                float value = PlayerPrefs.GetFloat(name);
                print(value);
                mixer.SetFloat(name, value);
            }
        }
    }

    public void OnPlayClicked()
    {
        menuCanvas.SetActive(false);
        settingsCanvas.SetActive(false);
        loadingCnavas.SetActive(false);
        controlsCanvas.SetActive(true);
        StartCoroutine(ControlsCoroutine());
    }

    IEnumerator ControlsCoroutine()
    {
        yield return new WaitForSeconds(controlsDisplayTime);
        loadingCnavas.SetActive(true);
        controlsCanvas.SetActive(false);
        StartCoroutine(LoadGameScene());
    }

    IEnumerator LoadGameScene()
    {
        AsyncOperation loadingOperation = SceneManager.LoadSceneAsync("GameScene");
        while (!loadingOperation.isDone)
        {
            loadingBar.value = Mathf.Clamp01(loadingOperation.progress / 0.9f);
            yield return null;
        }
    }

    public void OnSettingsClicked()
    {
        menuCanvas.SetActive(false);
        settingsCanvas.SetActive(true);
        creditsCnavas.SetActive(false);
    }

    public void OnCreditsClicked()
    {
        menuCanvas.SetActive(false);
        settingsCanvas.SetActive(false);
        creditsCnavas.SetActive(true);
    }

    public void OnBackClicked()
    {
        menuCanvas.SetActive(true);
        settingsCanvas.SetActive(false);
        creditsCnavas.SetActive(false);
        PlayerPrefs.Save();
    }

    public void OnExitClicked()
    {
        Application.Quit();
    }
}
