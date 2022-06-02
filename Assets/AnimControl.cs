using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimControl : MonoBehaviour
{

    private Animator anim;

    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    public void Jump()
    {
        anim.SetTrigger("Jump");
    }
}
