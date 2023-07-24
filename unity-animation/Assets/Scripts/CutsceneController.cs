using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class CutsceneController : MonoBehaviour
{
    public Animator animator;
    public UnityEvent OnAnimationEnd;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void AnimationEnd()
    {
        OnAnimationEnd.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
