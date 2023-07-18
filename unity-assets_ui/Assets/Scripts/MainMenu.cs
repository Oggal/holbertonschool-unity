using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public static int lastScene = 0;

    public static void LevelSelect(int targetSceneIndex)
    {
        MainMenu.lastScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(targetSceneIndex);
    }

    public void Options()
    {

    }

    public void Exit()
    {
        Debug.Log("Exit Game");
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }

}
