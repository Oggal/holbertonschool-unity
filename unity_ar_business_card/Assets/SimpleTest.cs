using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SimpleTest : MonoBehaviour
{
    public UnityEvent Activate;
    public UnityEvent Deactivate;

    void OnGUI()
    {
        if(GUILayout.Button("Activate"))
        {
            Activate.Invoke();
        }
        if(GUILayout.Button("Deactivate"))
        {
            Deactivate.Invoke();
        }
    }
}
