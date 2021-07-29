using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGameCountDown : MonoBehaviour
{
    public FishingMiniGame fishingMiniGame;
    public GameObject countDownText_obj;
    Text countDown_text;

    public bool startCountDown = false;
    float timer = 3;

    void Start()
    {
        countDown_text = countDownText_obj.GetComponent<Text>();
    }

    void Update()
    {
        if(startCountDown)
        {
            countDownText_obj.SetActive(true);
            timer -= Time.deltaTime;

            if (timer > 0)
            {
                countDown_text.text = "" + (Mathf.FloorToInt(timer) + 1);
            }
            else
            {
                timer = 3;
                startCountDown = false;
                countDownText_obj.SetActive(false);
                StartMiniGame();
            }
        }
    }

    void StartMiniGame()
    {
        fishingMiniGame.startMiniGame = true;
    }
}
