using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DailyStart : MonoBehaviour
{
    public int dayCount = 0;
    public float dailyTimeLimit = 500;
    public int dailyTargetCustomer = 20;

    float dayTimer = 0;

    public GameObject startUI_panel;
    public Text startUI_dayCount;
    public Text startUI_dailyTimeLimit;
    public Text startUI_dailyTargetCustomer;
    public Button startUI_startButton;
    public GameObject blackScreen;
    Animator startUI_anim;
    Image blackScreen_image;

    // Start is called before the first frame update
    void Start()
    {
        startUI_anim = startUI_panel.GetComponent<Animator>();
        blackScreen_image = blackScreen.GetComponent<Image>();
        InitiateScene();
    }

    // Update is called once per frame
    void Update()
    {
        startUI_dayCount.text = "Day " + dayCount;
        startUI_dailyTimeLimit.text = "Time\t:\t" + dailyTimeLimit;
        startUI_dailyTargetCustomer.text = "Target\t:\t" + dailyTargetCustomer;
    }

    void InitiateScene()
    {
        //stop time??
        blackScreen.SetActive(true);
        startUI_anim.SetBool("startUI_trigger", true);
    }

    public void StartDay()
    {
        //start time
        startUI_anim.SetBool("startUI_trigger", false);
        StartCoroutine(FadingBlackScreen(true));
    }

    public void Endday()
    {
        //stop time
        StartCoroutine(FadingBlackScreen(false));
    }

    IEnumerator FadingBlackScreen(bool trigger)
    {
        Color c = blackScreen_image.color;
        float elapsedTime = 0.0f;
        if (trigger == true)
        {
            while(elapsedTime < 1.5f)
            {
                elapsedTime += Time.deltaTime;
                c.a = 1.0f - Mathf.Clamp01(elapsedTime / 1.5f);
                blackScreen_image.color = c;
                yield return new WaitForEndOfFrame();
            }
            blackScreen.SetActive(false);
        }
        else
        {
            while (elapsedTime < 1.5f)
            {
                elapsedTime += Time.deltaTime;
                c.a = Mathf.Clamp01(elapsedTime / 1.5f);
                blackScreen_image.color = c;
                yield return new WaitForEndOfFrame();
            }
            startUI_anim.SetBool("startUI_trigger", true);
        }
    }
}
