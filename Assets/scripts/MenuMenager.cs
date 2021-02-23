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
    public GameObject levelSelector;
    public float controlsDisplayTime;
    public AudioMixer mixer;
    public UnityEngine.UI.Slider loadingBar;

    private readonly string[] prefsNames = { "MasterVolume", "SoundsVolume", "MusicVolume" };

    private void Start()
    {
        foreach (string name in prefsNames)
        {
            if (PlayerPrefs.HasKey(name))
            {
                float value = PlayerPrefs.GetFloat(name);
                mixer.SetFloat(name, value);
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            levelSelector.SetActive(true);
        }
    }

    public void OnPlayClicked()
    {
        menuCanvas.SetActive(false);
        settingsCanvas.SetActive(false);
        loadingCnavas.SetActive(false);
        controlsCanvas.SetActive(true);
        TMPro.TMP_InputField levelSelectorInput = levelSelector.GetComponent<TMPro.TMP_InputField>();
        int startLevel;
        if (int.TryParse(levelSelectorInput.text, out startLevel) && startLevel > 0 && startLevel <= 12)
        {
            PlayerPrefs.SetInt("StartLevel", startLevel - 1);
            PlayerPrefs.Save();
        }
        StartCoroutine(ControlsCoroutine());
    }

    IEnumerator ControlsCoroutine()
    {
        yield return new WaitForSecondsRealtime(controlsDisplayTime);
        loadingCnavas.SetActive(true);
        controlsCanvas.SetActive(false);
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
