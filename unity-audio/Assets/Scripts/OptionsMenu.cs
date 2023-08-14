using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class OptionsMenu : MonoBehaviour
{
    public static int lastScene = 0;

    public Slider BGM_slider, SFX_slider;
    public Toggle Y_Invert;

    public void Awake()
    {
        if (BGM_slider != null && PlayerPrefs.HasKey("BackgroundMusic"))
            BGM_slider.value = PlayerPrefs.GetFloat("BackgroundMusic");
        
        if (SFX_slider != null && PlayerPrefs.HasKey("SoundEffects"))
            SFX_slider.value = PlayerPrefs.GetFloat("SoundEffects");

        if (Y_Invert != null && PlayerPrefs.HasKey("Y_Invert"))
            Y_Invert.isOn = PlayerPrefs.GetInt("Y_Invert") == 1;
    }

    public void Back()
    {
        SceneManager.LoadScene(OptionsMenu.lastScene);
    }

    public void Apply()
    {
        if (BGM_slider != null)
            PlayerPrefs.SetFloat("BackgroundMusic", BGM_slider.value);
        if (SFX_slider != null)
            PlayerPrefs.SetFloat("SoundEffects",SFX_slider.value);
        if (Y_Invert != null)
            PlayerPrefs.SetInt("Y_Invert",Y_Invert.isOn? 1 : 0);

        SceneManager.LoadScene(OptionsMenu.lastScene);
    }
    
}
