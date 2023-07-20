using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] TMP_Text TimerText, WinTimeText;

    TimeSpan timePassed = TimeSpan.Zero;
    DateTime startTime;
    bool running = false;
    public UnityEngine.Events.UnityEvent OnWin;

    // Update is called once per frame
    void Update()
    {
        if(running)
            UpdateTimerText();
    }

    void UpdateTimerText()
    {
        if( TimerText == null || startTime == null)
            return;
        TimeSpan timeSpan = DateTime.Now - startTime;
        
        TimerText.text = (timeSpan.Add(timePassed).ToString("mm':'ss'.'ff"));
    }

    public void startTimer()
    {
        timePassed = TimeSpan.Zero;
        startTime = DateTime.Now;
        running = true;
    }

    public void Pause()
    {
        if(!running)
            return;
        TimeSpan span = DateTime.Now - startTime;
        timePassed += span;
        
    }

    public void Resume()
    {
        if (running)
        {
            startTime = DateTime.Now;
        }
    }

    public void Win()
    {
        TimeSpan final = DateTime.Now - startTime;
        final += timePassed;
        WinTimeText.text = final.ToString("mm':'ss'.'ff");
        TimerText.gameObject.SetActive(false);
        OnWin.Invoke();
        running = false;
    }
}
