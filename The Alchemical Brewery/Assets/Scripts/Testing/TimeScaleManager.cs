using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeScaleManager : MonoBehaviour
{
    private bool pauseGame;
    private bool accelerateGame;
    private float pauseGameScale;
    private float accelerateGameScale;

    void Start()
    {
        InitializeTimeScale();
    }

    void Update()
    {
        CheckingPauseState();
        CheckingAccelerateState();
    }

    private void InitializeTimeScale()
    {
        pauseGame = StageManager.pauseGame;
        int boolInt_pause = pauseGame ? 0 : 1;
        pauseGameScale = (float)boolInt_pause;

        accelerateGame = StageManager.accelerateGame;
        int boolInt_acce = accelerateGame ? 2 : 1;
        accelerateGameScale = (float)boolInt_acce;

        Time.timeScale = 1 * pauseGameScale * accelerateGameScale;
    }

    private void CheckingPauseState()
    {
        if (pauseGame != StageManager.pauseGame)
        {
            pauseGame = StageManager.pauseGame;
            int boolInt = pauseGame ? 0 : 1;
            pauseGameScale = (float)boolInt;
            TimeScaleChange(pauseGameScale, accelerateGameScale);
        }
    }

    private void CheckingAccelerateState()
    {
        if (accelerateGame != StageManager.accelerateGame)
        {
            Debug.Log("YESSSS");
            accelerateGame = StageManager.accelerateGame;
            int boolInt = accelerateGame ? 2 : 1;
            accelerateGameScale = (float)boolInt;
            TimeScaleChange(pauseGameScale, accelerateGameScale);
        }
    }

    private void TimeScaleChange(float _pauseGameScale, float _accelerateGameScale)
    {
        Time.timeScale = 1 * _pauseGameScale * _accelerateGameScale;
    }
}
