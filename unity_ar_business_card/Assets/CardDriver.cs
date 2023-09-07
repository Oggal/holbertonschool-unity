using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDriver : MonoBehaviour
{
    Animator[] animators;
    // Start is called before the first frame update
    void OnEnabled()
    { 
        foreach (Animator animator in animators)
        {
            animator.speed = 1;
            animator.StartPlayback();
        }
    }

    // Update is called once per frame
    void OnDisabled()
    {

    }
}
