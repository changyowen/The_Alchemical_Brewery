using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiPulsingScript : MonoBehaviour
{
    Animator UIanim;

    // Start is called before the first frame update
    void Start()
    {
        UIanim = GetComponent<Animator>();
    }

    public void MagnifyingFunction()
    {
        UIanim.SetBool("Magnify", true);
    }

    public void NormalFunction()
    {
        UIanim.SetBool("Magnify", false);
    }
}
