using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScreenScript : MonoBehaviour
{
    public static int nextSceneIndex = 0;

    public GameObject[] witchGirlAnimation_obj;

    public void Start()
    {
        ChangeBGM();
        RandomAnimation();
        StartCoroutine(NextScene());
    }

    IEnumerator NextScene()
    {
        //Start loading next scene
        AsyncOperation operation = SceneManager.LoadSceneAsync(nextSceneIndex);
        operation.allowSceneActivation = false;

        float tempTimer = 0;
        while (operation.progress >= 0.9f)
        {
            tempTimer += Time.unscaledDeltaTime;
            yield return null;
        }

        while(tempTimer < 5f)
        {
            tempTimer += Time.unscaledDeltaTime;
            yield return null;
        }

        operation.allowSceneActivation = true;
    }

    void RandomAnimation()
    {
        int temp = Random.Range(0, witchGirlAnimation_obj.Length);
        witchGirlAnimation_obj[temp].SetActive(true);
    }

    void ChangeBGM()
    {
        GlobalSoundAudio _globalSoundAudio = FindObjectOfType<GlobalSoundAudio>();
        if(_globalSoundAudio != null)
        {
            DetermineBGM(_globalSoundAudio);
        }
    }

    void DetermineBGM(GlobalSoundAudio _globalSoundAudio)
    {
        switch(nextSceneIndex)
        {
            case 0: //main menu
                {
                    StartCoroutine(_globalSoundAudio.ChangeNewMusic(SoundManager.mainMenuBGM, 2, 0.8f));
                    break;
                }
            case 2: //NTS
                {
                    StartCoroutine(_globalSoundAudio.ChangeNewMusic(SoundManager.nTS_lobby, 2, 0.5f));
                    break;
                }
            case 3: //ChosenPotionScene
                {
                    StartCoroutine(_globalSoundAudio.ChangeNewMusic(SoundManager.choosePotionBGM, 2, 1f));
                    break;
                }
            case 4: //stage 1
                {
                    StartCoroutine(_globalSoundAudio.FadeOutMusic(1f));
                    break;
                }
            case 5: //stage 2
                {
                    StartCoroutine(_globalSoundAudio.FadeOutMusic(1f));
                    break;
                }
            case 6: //stage 3
                {
                    StartCoroutine(_globalSoundAudio.FadeOutMusic(1f));
                    break;
                }
            case 7: //credit
                {
                    StartCoroutine(_globalSoundAudio.ChangeNewMusic(SoundManager.creditsBGM, 2, 0.8f));
                    break;
                }
        }
        
    }
}
