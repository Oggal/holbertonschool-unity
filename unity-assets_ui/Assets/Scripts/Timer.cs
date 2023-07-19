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
    bool running = false;
    public UnityEngine.Events.UnityEvent OnWin;

    // Update is called once per frame
    void Update()
    {
        UpdateTimerText();
    }

    void UpdateTimerText()
    {
        if( TimerText == null || startTime == null)
            return;
        System.TimeSpan timeSpan = System.DateTime.Now - startTime;
        
        TimerText.text = (timeSpan.Add(timePassed).ToString("mm':'ss'.'ff"));
    }

    public void startTimer()
    {
        timePassed = System.TimeSpan.Zero;
        startTime = System.DateTime.Now;
        enabled = true;
        running = true;
    }

    public void Pause()
    {
        enabled = false;
        if(!running)
            return;
        System.TimeSpan span = System.DateTime.Now - startTime;
        timePassed += span;
        
    }

    public void Resume()
    {
        if (running)
        {
            startTime = System.DateTime.Now;
            enabled = true;
        }
    }

    public void Win()
    {
        TimeSpan final = System.DateTime.Now - startTime;
        final += timePassed;
        WinTimeText.text = final.ToString("mm':'ss'.'ff");
        TimerText.gameObject.SetActive(false);
        OnWin.Invoke();
        running = false;
    }
}
