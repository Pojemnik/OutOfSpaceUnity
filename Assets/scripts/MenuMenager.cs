﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuMenager : MonoBehaviour
{
    public GameObject menuCanvas;
    public GameObject loadingCnavas;
    public GameObject creditsCnavas;
    public UnityEngine.UI.Slider loadingBar;

    public void OnPlayClicked()
    {
        menuCanvas.SetActive(false);
        loadingCnavas.SetActive(true);
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

    public void OnCreditsClicked()
    {
        menuCanvas.SetActive(false);
        creditsCnavas.SetActive(true);
    }

    public void OnBackClicked()
    {
        menuCanvas.SetActive(true);
        creditsCnavas.SetActive(false);
    }

    public void OnExitClicked()
    {
        Application.Quit();
    }
}