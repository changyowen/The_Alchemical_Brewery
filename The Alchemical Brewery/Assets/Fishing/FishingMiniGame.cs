using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingMiniGame : MonoBehaviour
{
    public static FishingMiniGame Instance { get; private set; }

    public Hook hook;
    public FishAI fishAI;
    public GameObject fishingMiniGame_parent_obj;
    public GameObject rendercanvas_obj;
    public Transform progressBarCenter;
    public GameObject countDown_obj;

    public bool startMiniGame = false;
    public float countDownTime = 8f;
    float miniGameTimer = 0f;
    public bool inHook = false;
    [Range(0, 100)] public float progress = 100;
    bool perfectResult = false;

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if(startMiniGame)
        {
            if(miniGameTimer < countDownTime)
            {
                miniGameTimer += Time.deltaTime;
            }
            else
            {
                MiniGameResult();
            }

            if (!inHook)
            {
                progress -= 15f * Time.deltaTime;
            }

            MiniGameProgress();
        }
    }

    void MiniGameProgress()
    {
        if (progress > 0f)
        {
            progressBarCenter.localScale = new Vector3(1f, (progress / 100f), 1f);
        }
        else if(progress >= 100f)
        {
            progressBarCenter.localScale = new Vector3(1f, 1f, 1f);
        }
        else
        {
            MiniGameResult();
        }
    }

    void MiniGameResult()
    {
        //get potion data from craft potion manager's potion holder
        PotionData newPotionData = CraftPotionManager.Instance.potionDataHolder;
        //assign progress into potion data quality
        newPotionData.potionQuality = progress;

        //reset all changes in minigame
        ResetAllMiniGameValue();

        //show animationn a bit
        
        //close fish minigame
        SetActiveMiniGameParent(false);
        //reset potIngredientList
        CraftPotionManager.Instance.ResetPotIngredientList();

        //return potion data into craftPotion manager result
        CraftPotionManager.Instance.CraftPotionResult(newPotionData);
    }

    void ResetFishingMiniGameValue()
    {
        inHook = false;
        miniGameTimer = 0;
        startMiniGame = false;
        progress = 100;
        perfectResult = false;

        progressBarCenter.localScale = Vector3.one;
    }

    public void ResetAllMiniGameValue()
    {
        ResetFishingMiniGameValue();
        fishAI.ResetFishAIValue();
        hook.ResetHookLocation();
    }

    public void StartMiniGame()
    {
        //miniGameCountDown.startCountDown = true;
        SetActiveMiniGameParent(true);
        //start count down
        StartCoroutine(StartCountDown());
    }

    IEnumerator StartCountDown()
    {
        //activate count down
        countDown_obj.SetActive(true);

        float tempTimer = 4;
        while(tempTimer > 1) //if count down havent finished
        {
            tempTimer -= Time.deltaTime;

            int timeLeft = (int)tempTimer;
            countDown_obj.GetComponent<TextMesh>().text = "" + timeLeft;
            yield return null;
        }

        //start fishing game
        startMiniGame = true;

        //and count down GO!
        while (tempTimer > 0)
        {
            tempTimer -= Time.deltaTime;
            countDown_obj.GetComponent<TextMesh>().text = "GO!";
            yield return null;
        }

        //deactivate countdown
        countDown_obj.SetActive(false);
    }

    public void SetActiveMiniGameParent(bool active)
    {
        fishingMiniGame_parent_obj.SetActive(active);
        rendercanvas_obj.SetActive(active);
    }

    
}
