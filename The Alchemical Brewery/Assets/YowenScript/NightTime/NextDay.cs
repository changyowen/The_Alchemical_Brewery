﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextDay : MonoBehaviour
{
    public void StartNextDay()
    {
        SceneManager.LoadScene(1);
    }

    public void BrewNewPotion()
    {
        SceneManager.LoadScene(3);
    }
}
