using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition : MonoBehaviour
{
    private Animator animator;
    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }
    public void FadeIn()
    { 
        animator.SetInteger("Transition", 2);
    }

    public void ToHidden() {
        animator.SetInteger("Transition", 3);
    }

    public void FadeOut() {
        animator.SetInteger("Transition", 1);
    }
}
