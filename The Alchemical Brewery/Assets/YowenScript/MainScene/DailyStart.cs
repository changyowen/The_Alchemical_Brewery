using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DailyStart : MonoBehaviour
{
    //potion (will improve later)
    public int[] potionToday1 = { 0, 0, 0, 0 };
    public int[] potionToday2 = { 0, 0, 0, 0 };
    public int[] potionToday3 = { 0, 0, 0, 0 };
    public int[] potionToday4 = { 0, 0, 0, 0 };

    //system data
    public int dayCount = 0;
    public float dailyTimeLimit = 500;
    public int dailyTargetCustomer = 20;
    public static int dailyServedCustomer = 0;
    public static int dailyAngryCustomer = 0;
    public static int dailyEarnedMoney = 0;

    //system trigger
    float dayTimer = 0;
    bool startEndScene = false;

    //access UI
    public Text servedCustomerUI;
    public Text earnedMoneyUI;

    //access panel
    public GameObject startUI_panel;
    public GameObject endUI_panel;
    public Text startUI_dayCount;
    public Text startUI_dailyTimeLimit;
    //public Text startUI_dailyTargetCustomer;
    public Button startUI_startButton;
    public Text endUI_dayCount;
    public Text endUI_servedCustomer;
    public Text endUI_angryCustomer;
    public GameObject blackScreen;
    Animator startUI_anim, endUI_anim;
    Image blackScreen_image;

    // Start is called before the first frame update
    void Start()
    {
        startUI_anim = startUI_panel.GetComponent<Animator>();
        endUI_anim = endUI_panel.GetComponent<Animator>();
        blackScreen_image = blackScreen.GetComponent<Image>();
        InitiateScene();
    }

    // Update is called once per frame
    void Update()
    {
        if(startEndScene == true)
        {
            //startUI
            startUI_dayCount.text = "Day " + dayCount;
            startUI_dailyTimeLimit.text = "Time\t:\t" + dailyTimeLimit;
            //startUI_dailyTargetCustomer.text = "Target\t:\t" + dailyTargetCustomer;
            //endUI
            endUI_dayCount.text = "Day " + dayCount + " Ended";
            endUI_servedCustomer.text = ":\t" + dailyServedCustomer;
            endUI_angryCustomer.text = ":\t" + dailyAngryCustomer;
            
        }
        else
        {
            dayTimer += Time.deltaTime;

            //InGameUI
            servedCustomerUI.text = "" + dailyServedCustomer;
            earnedMoneyUI.text = "" + dailyEarnedMoney;

            if (dayTimer >= dailyTimeLimit)
            {
                Endday();
            }
        }
    }

    void InitiateScene()
    {
        //stop time??
        startEndScene = true;
        blackScreen.SetActive(true);
        startUI_anim.SetBool("startUI_trigger", true);
        endUI_anim.SetBool("startUI_trigger", false);

        dailyAngryCustomer = 0;
    }

    public void StartDay()
    {
        //start time
        dayTimer = 0;
        startUI_anim.SetBool("startUI_trigger", false);
        StartCoroutine(FadingBlackScreen(true));
    }

    public void Endday()
    {
        //stop time
        dayTimer = 0;
        startEndScene = true;
        blackScreen.SetActive(true);
        StartCoroutine(FadingBlackScreen(false));
    }

    public void CloseShop()
    {
        SceneManager.LoadScene(1);
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
            startEndScene = false;
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
            endUI_anim.SetBool("startUI_trigger", true);
        }
    }
}
