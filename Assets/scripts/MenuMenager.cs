using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuMenager : MonoBehaviour
{
    public void OnPlayClicked()
    {
        SceneManager.LoadSceneAsync("GameScene");
    }

    public void OnCreditsClicked()
    {

    }

    public void OnExitClicked()
    {
        Application.Quit();
    }
}
