using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WinTrigger : MonoBehaviour
{
    public UnityEvent OnWin;
    void OnTriggerEnter(Collider other)
    {
        Timer otherTimer = other.GetComponent<Timer>();
        if (otherTimer != null)
        {
                otherTimer.Win();
                OnWin.Invoke();
        }
    }
}
