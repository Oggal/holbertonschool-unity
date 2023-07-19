using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinMenu : MonoBehaviour
{
    public void Next()
    {
        SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex + 1 ) % SceneManager.sceneCountInBuildSettings);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
