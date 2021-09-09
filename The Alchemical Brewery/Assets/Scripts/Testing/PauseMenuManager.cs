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
            GlobalSoundAudio _globalSoundAudio = FindObjectOfType<GlobalSoundAudio>();
            if (_globalSoundAudio != null)
            {
                StartCoroutine(_globalSoundAudio.FadeOutMusic(1f));
            }
            StageManager.pauseGame = true;
            pausePanel_obj.SetActive(true);
        }
    }

    public void ResumeGame()
    {
        GlobalSoundAudio _globalSoundAudio = FindObjectOfType<GlobalSoundAudio>();
        if (_globalSoundAudio != null)
        {
            StartCoroutine(_globalSoundAudio.FadeInMusic(1f, 0.6f));
        }
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
        Time.timeScale = 1;
        LoadingScreenScript.nextSceneIndex = 0;
        AsyncOperation operation = SceneManager.LoadSceneAsync(1);
    }

    public void NotQuitGame()
    {
        confirmQuitPanel_obj.SetActive(false);
        pausePanel_obj.SetActive(true);
    }
}
