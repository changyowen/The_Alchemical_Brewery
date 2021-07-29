using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingMiniGame : MonoBehaviour
{
    public CraftPotionManager craftPotionManager;
    public Hook hook;
    public FishAI fishAI;
    public MiniGameCountDown miniGameCountDown;
    public GameObject fishingMiniGame_parent_obj;
    public Transform progressBarCenter;

    public bool startMiniGame = false;
    public float progress = 0;
    bool perfectResult = false;

    void Update()
    {
        if(startMiniGame)
        {
            MiniGameProgress();
        }
    }

    void MiniGameProgress()
    {
        if (progress < 100f)
        {
            progressBarCenter.localScale = new Vector3(1f, (progress / 100f), 1f);
        }
        else
        {
            progress = 100f;
            startMiniGame = false;
            MiniGameResult();
        }
    }

    void MiniGameResult()
    {
        //get and show result
        //show animationn a bit
        //reset all changes in minigame
        //close fish minigame
        SetActiveMiniGameParent(false);
        //set active craftPotionUI
        //craftPotionManager.SetActiveCraftPotionUI(true);
        //reset potIngredientList
        //craftPotionManager.ResetPotIngredientList();
    }

    void ResetFishingMiniGameValue()
    {
        startMiniGame = false;
        progress = 0;
        perfectResult = false;
    }

    public void ResetAllMiniGameValue()
    {
        ResetFishingMiniGameValue();
        fishAI.ResetFishAIValue();
        hook.ResetHookLocation();
    }

    public void SetActiveMiniGameParent(bool active)
    {
        fishingMiniGame_parent_obj.SetActive(active);
    }

    public void StartMiniGame()
    {
        miniGameCountDown.startCountDown = true;
    }
}
