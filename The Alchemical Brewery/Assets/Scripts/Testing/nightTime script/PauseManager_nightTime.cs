using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager_nightTime : MonoBehaviour
{
    public GameObject NTS_pausePanel_obj;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            NTS_pausePanel_obj.SetActive(true);
        }
    }

    public void ClosePausePanel()
    {
        NTS_pausePanel_obj.SetActive(false);
    }

    public void ExitToMenu()
    {
        Time.timeScale = 1;
        LoadingScreenScript.nextSceneIndex = 0;
        AsyncOperation operation = SceneManager.LoadSceneAsync(1);
    }
}
