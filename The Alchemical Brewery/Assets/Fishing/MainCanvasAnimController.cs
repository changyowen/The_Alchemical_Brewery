using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCanvasAnimController : MonoBehaviour
{
    Animator mainCanvasAnim;

    void Start()
    {
        mainCanvasAnim = GetComponent<Animator>();
    }

    public void OpenCraftPotionScene()
    {
        mainCanvasAnim.SetTrigger("craftPotionScene_Open");
    }
}
