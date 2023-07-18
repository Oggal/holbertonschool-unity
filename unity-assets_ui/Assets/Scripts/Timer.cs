using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] TMPro.TMP_Text TimerText, WinTimeText;

    System.TimeSpan timePassed = System.TimeSpan.Zero;
    System.DateTime startTime;
    

    void OnEnable()
    {
        startTime = System.DateTime.Now;

    }

    void OnDisable()
    {
        TimerText.color = Color.green;
        TimerText.fontSize = 60;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTimerText();
    }

    void UpdateTimerText()
    {
        if( TimerText == null)
            return;
        System.TimeSpan timeSpan = System.DateTime.Now - startTime;
        
        TimerText.text = (timeSpan.Add(timePassed).ToString("mm':'ss'.'ff"));
    }

    void startTimer()
    {
        timePassed = System.TimeSpan.Zero;
        startTime = System.DateTime.Now;
    }

    void Pause()
    {
        System.TimeSpan span = System.DateTime.Now - startTime;
        timePassed += span;
    }

    void Resume()
    {
        startTime = System.DateTime.Now;
    }

    public void Win()
    {
        TimeSpan final = System.DateTime.Now - startTime;
        final += timePassed;
        WinTimeText.text = final.ToString("mm':'ss'.'ff");
        TimerText.gameObject.SetActive(false);
    }
}
