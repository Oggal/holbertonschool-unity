using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class OptionsMenu : MonoBehaviour
{
    public static int lastScene = 0;

    
    [SerializeField] Slider BGM_slider, SFX_slider;

    public Toggle Y_Invert;

    [Header("Audio Mixer")]
    public AudioMixer Mixer;

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
            SetBGM();
        if (SFX_slider != null)
            SetSFX();
        if (Y_Invert != null)
            PlayerPrefs.SetInt("Y_Invert",Y_Invert.isOn? 1 : 0);

        SceneManager.LoadScene(OptionsMenu.lastScene);
    }

    void SetBGM()
    {
        if (BGM_slider == null)
            return;
        PlayerPrefs.SetFloat("BackgroundMusic", BGM_slider.value);

        if (Mixer != null)
        {
            //Set output volume for mixer group
            float newVolume = LinearToDecibel(BGM_slider.value);
            Mixer.SetFloat("BGM_Vol", newVolume);
        }

    }

        void SetSFX()
    {
        if (SFX_slider == null)
            return;
        PlayerPrefs.SetFloat("SoundEffects", SFX_slider.value);

        if (Mixer != null)
        {
            //Set output volume for mixer group
            float newVolume = LinearToDecibel(SFX_slider.value);
            Mixer.SetFloat("SFX_Vol", newVolume);
        }

    }

    private float LinearToDecibel(float linear)
	{
		float dB;
		
		if (linear != 0)
			dB = 20.0f * Mathf.Log10(linear);
		else
			dB = -144.0f;
		
		return dB;
	}

    private float DecibelToLinear(float dB)
	{
		float linear = Mathf.Pow(10.0f, dB/20.0f);

		return linear;
	}
    
}
