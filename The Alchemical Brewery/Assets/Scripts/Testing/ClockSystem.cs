﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockSystem : MonoBehaviour
{
    public static ClockSystem Instance { get; private set; }

    public GameObject clockhand_minute_obj;
    public GameObject clockhand_hour_obj;

    [Range(0, 24)] public float TimeOfDay = 8;
    [SerializeField] private float timeSpeedperHour;

    void Awake()
    {
        Instance = this;
        TimeOfDay = 8f;
    }

    void Update()
    {
        if(StageManager.dayTimeGameplay)
        {
            TimeCalculation();
            ClockHandRotation();
        }
    }

    void TimeCalculation()
    {
        if(!StageManager.accelerateGame)
        {
            TimeOfDay += Time.deltaTime * timeSpeedperHour;
        }
        else
        {
            TimeOfDay += Time.unscaledDeltaTime * timeSpeedperHour;
        }
    
        TimeOfDay %= 24; //Modulus to ensure always between 0-24
    }

    void ClockHandRotation()
    {
        float rotationDegreesPerHour_minute = 360f;
        clockhand_minute_obj.transform.eulerAngles = new Vector3(0, 0, -TimeOfDay * rotationDegreesPerHour_minute);

        float rotationDegreesPerHour_hour = 30f;
        clockhand_hour_obj.transform.eulerAngles = new Vector3(0, 0, -TimeOfDay * rotationDegreesPerHour_hour);
    }
}
