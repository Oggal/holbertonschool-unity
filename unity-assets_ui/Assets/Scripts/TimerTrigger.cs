using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerTrigger : MonoBehaviour
{
    void OnTriggerExit(Collider other)
    {
        Timer otherTimer = other.GetComponent<Timer>();
        if (otherTimer != null)
            otherTimer.enabled = true;
    }
}
