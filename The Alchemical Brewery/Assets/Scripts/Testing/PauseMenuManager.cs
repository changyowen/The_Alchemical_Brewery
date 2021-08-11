using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel_obj;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(StageManager.pauseGame)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        StageManager.pauseGame = true;
        pausePanel_obj.SetActive(true);
    }

    public void ResumeGame()
    {
        StageManager.pauseGame = false;
        pausePanel_obj.SetActive(false);
    }

    public void EndDay()
    {

    }

    public void ExitToMenu()
    {

    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
