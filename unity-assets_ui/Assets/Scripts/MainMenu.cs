using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void LevelSelect(int targetSceneIndex)
    {
        OptionsMenu.lastScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(targetSceneIndex);
    }

    public void Options()
    {
        OptionsMenu.lastScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        Debug.Log("Exited");
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }

}
