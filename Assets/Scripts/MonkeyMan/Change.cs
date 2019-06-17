using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Change : MonoBehaviour
{
    private int q = Animator.StringToHash("q");
    private int e = Animator.StringToHash("e");

    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetButtonDown("q"))
        {
            anim.SetBool(q, !anim.GetBool("q"));
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("New Animation"))
            {
                
            }
        }
        if (Input.GetButtonDown("e"))
        {
            anim.SetBool(e, !anim.GetBool("e"));

        }
    }
}
