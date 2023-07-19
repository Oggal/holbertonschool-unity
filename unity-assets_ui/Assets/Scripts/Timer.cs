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
    public UnityEngine.Events.UnityEvent OnWin;

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

    public void startTimer()
    {
        timePassed = System.TimeSpan.Zero;
        startTime = System.DateTime.Now;
        enabled = true;
    }

    public void Pause()
    {
        System.TimeSpan span = System.DateTime.Now - startTime;
        timePassed += span;
        enabled = false;
    }

    void Resume()
    {
        startTime = System.DateTime.Now;
        enabled = true;
    }

    public void Win()
    {
        TimeSpan final = System.DateTime.Now - startTime;
        final += timePassed;
        WinTimeText.text = final.ToString("mm':'ss'.'ff");
        TimerText.gameObject.SetActive(false);
        OnWin.Invoke();
    }
}
