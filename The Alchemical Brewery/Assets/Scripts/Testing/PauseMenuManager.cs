using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel_obj = null;
    [SerializeField] private GameObject confirmQuitPanel_obj = null;

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
        if(!StageManager.pauseGame && StageManager.dayTimeGameplay)
        {
            StageManager.pauseGame = true;
            pausePanel_obj.SetActive(true);
        }
    }

    public void ResumeGame()
    {
        StageManager.pauseGame = false;
        pausePanel_obj.SetActive(false);
    }

    public void CloseShop()
    {
        StageManager.pauseGame = false;
        StartCoroutine(StageManager.Instance.EndDayTimeGamePlay());
    }

    public void ExitToMenu()
    {
        pausePanel_obj.SetActive(false);
        confirmQuitPanel_obj.SetActive(true);
    }

    public void ConfirmQuit()
    {
        LoadingScreenScript.nextSceneIndex = 1;
        AsyncOperation operation = SceneManager.LoadSceneAsync(0);
    }

    public void NotQuitGame()
    {
        confirmQuitPanel_obj.SetActive(false);
        pausePanel_obj.SetActive(true);
    }
}
