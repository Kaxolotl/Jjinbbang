using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HamsterAnimationControl : MonoBehaviour {

    public Animator animator;
    public SpriteRenderer sprite;

    void Start ()
    {
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    public void PlayMoveAnimation(bool flag)
    {
        animator.SetBool("walking", flag);
    }
}
