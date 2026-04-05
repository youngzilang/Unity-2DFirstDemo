using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeScreenUI : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void FadeOut() => animator.SetTrigger("fadeOut");

    public void FadeIn() => animator.SetTrigger("fadeIn");
}
