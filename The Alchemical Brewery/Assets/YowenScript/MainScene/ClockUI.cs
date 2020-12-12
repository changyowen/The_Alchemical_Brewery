using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClockUI : MonoBehaviour
{
    private const float REAL_SECONDS_PER_INGAME_DAY = 180f;

    public Transform HourHandTransform;
    public Transform MinuteHandTransform;
    public GameObject ClockCenter;
    private float day;

    public bool clockTrigger = false;

    public GameObject DailySystem_gameObject;
    DailyStart dailyStart;

    void Start()
    {
        dailyStart = DailySystem_gameObject.GetComponent<DailyStart>();
    }

    // Update is called once per frame
    void Update()
    {
        if(dailyStart.startEndScene == false)
        {
            clockTrigger = true;
        }

        if(clockTrigger == true)
        {
            day += Time.deltaTime / REAL_SECONDS_PER_INGAME_DAY;

            float dayNormalized = day % 1f;

            float rotationDegreesPerday = 360f;
            HourHandTransform.eulerAngles = new Vector3(0, 0, -dayNormalized * rotationDegreesPerday);

            float hoursPerDay = 24f;
            MinuteHandTransform.eulerAngles = new Vector3(0, 0, -dayNormalized * rotationDegreesPerday * hoursPerDay);

            SycnTime();
        }
    }

    void SycnTime()
    {
        float gameTime = dailyStart.dayTimer;
        float percentage = (dailyStart.dailyTimeLimit - gameTime) / dailyStart.dailyTimeLimit;
        ClockCenter.transform.localScale = new Vector3(percentage, 1, 1);

        if(percentage < .5f && percentage > .1f)
        {
            ClockCenter.transform.GetChild(0).GetComponent<Image>().color = Color.yellow;
        }
        else if(percentage < .1f)
        {
            ClockCenter.transform.GetChild(0).GetComponent<Image>().color = Color.red;
        }
    }

    public void resetTime()
    {
        //
    }
}
