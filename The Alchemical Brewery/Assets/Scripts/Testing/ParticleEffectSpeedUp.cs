using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEffectSpeedUp : MonoBehaviour
{
    public ParticleSystem[] allPS;
    public float hSliderValue = 1.0F;

    void OnEnable()
    {
        for (int i = 0; i < allPS.Length; i++)
        {
            ParticleSystem.MainModule main = allPS[i].main;
            main.simulationSpeed = hSliderValue;
        }
    }

    void OnGUI()
    {
        hSliderValue = GUI.HorizontalSlider(new Rect(25, 45, 100, 30), hSliderValue, 0.0F, 5.0F);
    }
}
