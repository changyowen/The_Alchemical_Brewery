using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateDrivenHandler : MonoBehaviour
{
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            anim.Play("mainRoom");
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            anim.Play("potRoom");
        }
    }
}
