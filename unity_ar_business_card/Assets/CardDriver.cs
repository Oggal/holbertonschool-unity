using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDriver : MonoBehaviour
{
    [SerializeField] Animator[] animators;
    // Start is called before the first frame update
    void Start()
    {
        animators = GetComponentsInChildren<Animator>();
        foreach(Animator anim in animators)
        {
            anim.enabled = false;
        }
    }

    public void OnEnable()
    { 
        foreach (Animator animator in animators)
        {
            animator.enabled = true;
        }
    }

    // Update is called once per frame
    public void OnDisable()
    {
        foreach (Animator animator in animators)
        {
            animator.enabled = false;
        }
    }
}
