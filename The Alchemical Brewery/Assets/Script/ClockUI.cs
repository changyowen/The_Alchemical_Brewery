using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockUI : MonoBehaviour
{
    private const float REAL_SECONDS_PER_INGAME_DAY = 180f;

    public Transform HourHandTransform;
    public Transform MinuteHandTransform;
    private float day;

    bool clockTrigger = false;

    // Update is called once per frame
    void Update()
    {
        if(clockTrigger == true)
        {
            day += Time.deltaTime / REAL_SECONDS_PER_INGAME_DAY;

            float dayNormalized = day % 1f;

            float rotationDegreesPerday = 360f;
            HourHandTransform.eulerAngles = new Vector3(0, 0, -dayNormalized * rotationDegreesPerday);

            float hoursPerDay = 24f;
            MinuteHandTransform.eulerAngles = new Vector3(0, 0, -dayNormalized * rotationDegreesPerday * hoursPerDay);
        }
    }

    public void resetTime()
    {
        //
    }
}
