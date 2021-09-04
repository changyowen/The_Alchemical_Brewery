using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClockSystem : MonoBehaviour
{
    public static ClockSystem Instance { get; private set; }

    public GameObject clockhand_minute_obj;
    public GameObject clockhand_hour_obj;
    public Text digitalClock_text; 

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
            DigitalTimeUpdate();
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

    void DigitalTimeUpdate()
    {
        int timeInInt = (int)TimeOfDay;
        if(timeInInt == 0)
        {
            digitalClock_text.text = "12 am";
        }
        else if(timeInInt > 0 && timeInInt < 12)
        {
            digitalClock_text.text = timeInInt + " am";
        }
        else if(timeInInt == 12)
        {
            digitalClock_text.text = "12 pm";
        }
        else if(timeInInt > 12)
        {
            digitalClock_text.text = (timeInInt - 12) + " pm";
        }
    }
}
